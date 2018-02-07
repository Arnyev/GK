#pragma once
#include <vector>
#include <Vertex.h>
#define TINYOBJLOADER_IMPLEMENTATION
#include <tiny_obj_loader.h>

using namespace std;

void LoadBlenderModel(char * path, vector<Vertex>& vertices, vector<uint32_t>& indices)
{
    std::vector< glm::vec2 > temp_uvs;
    std::vector< glm::vec3 > temp_normals;

    FILE * file = fopen(path, "r");
    char str[80];

    while (1)
    {
        char lineHeader[128];
        int res = fscanf(file, "%s", lineHeader);
        if (res == EOF)
            break; 

        if (strcmp(lineHeader, "v") == 0)
        {
            glm::vec3 vertex;
            fscanf(file, "%f %f %f\n", &vertex.x, &vertex.y, &vertex.z);
            vertices.push_back({ vertex });
        }
        else if (strcmp(lineHeader, "vt") == 0)
        {
            glm::vec2 uv;
            fscanf(file, "%f %f\n", &uv.x, &uv.y);
            temp_uvs.push_back(uv);
        }
        else if (strcmp(lineHeader, "vn") == 0)
        {
            glm::vec3 normal;
            fscanf(file, "%f %f %f\n", &normal.x, &normal.y, &normal.z);
            temp_normals.push_back(normal);
        }
        else if (strcmp(lineHeader, "f") == 0)
        {
            unsigned int vertexIndex[3], uvIndex[3], normalIndex[3];

            fgets(str, 80, file);

            int matches = sscanf(str, "%d//%d %d//%d %d//%d\n", &vertexIndex[0], &normalIndex[0], &vertexIndex[1], &normalIndex[1], &vertexIndex[2], &normalIndex[2]);
            if (matches == 6)
            {
                indices.push_back(vertexIndex[0]);
                indices.push_back(vertexIndex[1]);
                indices.push_back(vertexIndex[2]);

                vertices[vertexIndex[0] - 1].normal = temp_normals[normalIndex[0] - 1];
                vertices[vertexIndex[1] - 1].normal = temp_normals[normalIndex[1] - 1];
                vertices[vertexIndex[2] - 1].normal = temp_normals[normalIndex[2] - 1];
            }

            else
            {
                matches = sscanf(str, "%d/%d/%d %d/%d/%d %d/%d/%d\n", &vertexIndex[0], &uvIndex[0], &normalIndex[0], &vertexIndex[1], &uvIndex[1], &normalIndex[1], &vertexIndex[2], &uvIndex[2], &normalIndex[2]);
                if (matches == 9)
                {
                    indices.push_back(vertexIndex[0]);
                    indices.push_back(vertexIndex[1]);
                    indices.push_back(vertexIndex[2]);

                    vertices[vertexIndex[0] - 1].texCoord = temp_uvs[uvIndex[2] - 1];
                    vertices[vertexIndex[1] - 1].texCoord = temp_uvs[uvIndex[1] - 1];
                    vertices[vertexIndex[2] - 1].texCoord = temp_uvs[uvIndex[0] - 1];

                    vertices[vertexIndex[0] - 1].normal = temp_normals[normalIndex[0] - 1];
                    vertices[vertexIndex[1] - 1].normal = temp_normals[normalIndex[1] - 1];
                    vertices[vertexIndex[2] - 1].normal = temp_normals[normalIndex[2] - 1];
                }
            }
        }
    }
    for (int i = 0;i < indices.size();i++)
        --indices[i];
}

void LoadModel(char * path, vector<Vertex>& vertices, vector<uint32_t>& indices)
{
    tinyobj::attrib_t attrib;
    std::vector<tinyobj::shape_t> shapes;
    std::vector<tinyobj::material_t> materials;
    std::string err;

    if (!tinyobj::LoadObj(&attrib, &shapes, &materials, &err, path))
    {
        throw std::runtime_error(err);
    }

    std::unordered_map<Vertex, uint32_t> uniqueVertices = {};

    for (const auto& shape : shapes)
    {
        for (const auto& index : shape.mesh.indices)
        {
            Vertex vertex = {};
            vertex.pos = {
                attrib.vertices[3 * index.vertex_index + 0],
                attrib.vertices[3 * index.vertex_index + 1],
                attrib.vertices[3 * index.vertex_index + 2]
            };

            vertex.texCoord = {
                attrib.texcoords[2 * index.texcoord_index + 0],
                1.0f - attrib.texcoords[2 * index.texcoord_index + 1]
            };

            vertex.color = { 1.0f, 1.0f, 1.0f };

            vertex.normal = { attrib.normals[3 * index.normal_index + 0],
                attrib.normals[3 * index.normal_index + 1],
                attrib.normals[3 * index.normal_index + 2] };

            if (uniqueVertices.count(vertex) == 0)
            {
                uniqueVertices[vertex] = static_cast<uint32_t>(vertices.size());
                vertices.push_back(vertex);
            }

            indices.push_back(uniqueVertices[vertex]);
        }
    }
}

vector<char> readFile(const std::string& filename)
{
    std::ifstream file(filename, std::ios::ate | std::ios::binary);

    if (!file.is_open())
    {
        throw std::runtime_error("failed to open file!");
    }

    size_t fileSize = (size_t)file.tellg();
    std::vector<char> buffer(fileSize);

    file.seekg(0);
    file.read(buffer.data(), fileSize);

    file.close();

    return buffer;
}