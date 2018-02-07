#pragma once

#include <FileLoader.h>

using namespace std;

class ModelsWrapper
{
public:
    vector<Vertex> ZeroVertices;
    vector<uint32_t> ZeroIndices;

    vector<Vertex> OneVertices;
    vector<uint32_t> OneIndices;

    vector<Vertex> TwoVertices;
    vector<uint32_t> TwoIndices;

    vector<Vertex> ThreeVertices;
    vector<uint32_t> ThreeIndices;

    vector<Vertex> FourVertices;
    vector<uint32_t> FourIndices;

    vector<Vertex> FiveVertices;
    vector<uint32_t> FiveIndices;

    vector<Vertex> SixVertices;
    vector<uint32_t> SixIndices;

    vector<Vertex> SevenVertices;
    vector<uint32_t> SevenIndices;

    vector<Vertex> EightVertices;
    vector<uint32_t> EightIndices;

    vector<Vertex> NineVertices;
    vector<uint32_t> NineIndices;

    unordered_map<uint32_t, vector<Vertex>*> verticesMap;
    unordered_map<uint32_t, vector<uint32_t>*> indicesMap;

    vector<Vertex> FlashlightVertices;
    vector<uint32_t>  FlashlightIndices;

    vector<Vertex> ChurchVertices;
    vector<uint32_t>  ChurchIndices;

    vector<Vertex> FloorVertices;
    vector<uint32_t>  FloorIndices;

    vector<Vertex> BallVertices;
    vector<uint32_t>  BallIndices;

    ModelsWrapper()
    {
        InitializeModels();
        InitializeMaps();

        for (int i = 0;i < 10;i++)
        {
            auto vertices = verticesMap[i];
            for (int j = 0;j < vertices->size();j++)
            {
                vertices->at(j).pos /= 50;
                vertices->at(j).normal = glm::vec3(-1, 0, 0);
                vertices->at(j).TextureNr = 3;
            }
        }
        for (int j = 0;j < FlashlightVertices.size();j++)
            FlashlightVertices[j].pos /= 35;

    }

    void InitializeModels()
    {
        LoadBlenderModel("models/Zero.obj", ZeroVertices, ZeroIndices);
        LoadBlenderModel("models/One.obj", OneVertices, OneIndices);
        LoadBlenderModel("models/Two.obj", TwoVertices, TwoIndices);
        LoadBlenderModel("models/Three.obj", ThreeVertices, ThreeIndices);
        LoadBlenderModel("models/Four.obj", FourVertices, FourIndices);
        LoadBlenderModel("models/Five.obj", FiveVertices, FiveIndices);
        LoadBlenderModel("models/Six.obj", SixVertices, SixIndices);
        LoadBlenderModel("models/Seven.obj", SevenVertices, SevenIndices);
        LoadBlenderModel("models/Eight.obj", EightVertices, EightIndices);
        LoadBlenderModel("models/Nine.obj", NineVertices, NineIndices);

        LoadModel("models/Light.obj", FlashlightVertices, FlashlightIndices);
        LoadModel("models/church.obj", ChurchVertices, ChurchIndices);
        LoadModel("models/floor.obj", FloorVertices, FloorIndices);
        LoadModel("models/ball.obj", BallVertices, BallIndices);


        for (int i = 0;i < ChurchVertices.size();i++)
        {
            ChurchVertices[i].pos.y -= 10;
        }
        for (int i = 0;i < FloorVertices.size();i++)
        {
            FloorVertices[i].pos.x -= 2000;
            FloorVertices[i].pos.y -= 20;
        }
    }

    void InitializeMaps()
    {
        verticesMap[0] = &ZeroVertices;
        indicesMap[0] = &ZeroIndices;

        verticesMap[1] = &OneVertices;
        indicesMap[1] = &OneIndices;

        verticesMap[2] = &TwoVertices;
        indicesMap[2] = &TwoIndices;

        verticesMap[3] = &ThreeVertices;
        indicesMap[3] = &ThreeIndices;

        verticesMap[4] = &FourVertices;
        indicesMap[4] = &FourIndices;

        verticesMap[5] = &FiveVertices;
        indicesMap[5] = &FiveIndices;

        verticesMap[6] = &SixVertices;
        indicesMap[6] = &SixIndices;

        verticesMap[7] = &SevenVertices;
        indicesMap[7] = &SevenIndices;

        verticesMap[8] = &EightVertices;
        indicesMap[8] = &EightIndices;

        verticesMap[9] = &NineVertices;
        indicesMap[9] = &NineIndices;
    }

    void AppendModel(vector<Vertex> & destVertices, vector<uint32_t> & destIndices, vector<Vertex> & srcVertices, vector<uint32_t> & srcIndices, int modelNr, int textureNr)
    {
        int lastVerticesIndex = destVertices.size();
        int lastIndicesIndex = destIndices.size();

        destVertices.insert(end(destVertices), begin(srcVertices), end(srcVertices));
        destIndices.insert(end(destIndices), begin(srcIndices), end(srcIndices));

        for (int i = lastIndicesIndex;i < destIndices.size();++i)
            destIndices[i] += lastVerticesIndex;

        for (int i = lastVerticesIndex;i < destVertices.size();++i)
        {
            destVertices[i].ModelNr = modelNr;
            destVertices[i].TextureNr = textureNr;
        }
    }

    void AppendAllNumbers(vector<Vertex> & vertices, vector<uint32_t> & indices)
    {
        for (int digit = 0; digit < 10; ++digit)
            for (int j = 0; j < 5; ++j)
            {
                int lastVerticesIndex = vertices.size();
                int lastIndicesIndex = indices.size();

                vector<Vertex>& digitVertices(*verticesMap[digit]);
                vector<uint32_t>& digitIndices(*indicesMap[digit]);
                vertices.insert(end(vertices), begin(digitVertices), end(digitVertices));
                indices.insert(end(indices), begin(digitIndices), end(digitIndices));

                for (int i = lastIndicesIndex;i < indices.size();++i)
                    indices[i] += lastVerticesIndex;

                for (int i = lastVerticesIndex;i < vertices.size();++i)
                    vertices[i].ModelNr = 5 * digit + j + 1;
            }
    }

    void FillNumberModel(int number, vector<Vertex> & vertices, vector<uint32_t> & indices)
    {
        int currentDigitIndex = 0;
        while (number > 0)
        {
            int digit = number % 10;
            int lastVerticesIndex = vertices.size();
            int lastIndicesIndex = indices.size();

            vector<Vertex> digitVertices = *verticesMap[digit];
            vector<uint32_t> digitIndices = *indicesMap[digit];
            vertices.insert(end(vertices), begin(digitVertices), end(digitVertices));
            indices.insert(end(indices), begin(digitIndices), end(digitIndices));

            for (int i = lastIndicesIndex;i < indices.size();++i)
                indices[i] += lastVerticesIndex;

            for (int i = lastVerticesIndex;i < vertices.size();++i)
                vertices[i].pos.r += currentDigitIndex;

            number /= 10;
            ++currentDigitIndex;
        }
    }
};