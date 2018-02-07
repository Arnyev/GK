#pragma once

#define GLM_FORCE_RADIANS
#define GLM_FORCE_DEPTH_ZERO_TO_ONE
#define GLM_ENABLE_EXPERIMENTAL

#include <glm/gtc/matrix_transform.hpp>
#include <glm/gtx/hash.hpp>
#include <glm/gtx/rotate_vector.hpp>
#include "math.h"


using namespace glm;
using namespace std;
using namespace std::chrono;

struct UniformBufferObject
{
    mat4 models[64];
    mat4 view;
    mat4 proj;
    vec4 Lighthouse;
    vec4 spotExponent;
    vec4 constantAttenuation; // K0   
    vec4 linearAttenuation;   // K1   
    vec4 quadraticAttenuation;// K2  
    vec4 diffuse;              // Dcli   
    vec4 diffuseRatio;
    vec4 SpecularRatio;
    vec4 Type;

};

enum DirectionEnum
{
    NoDirection = 0,
    Forward = 1,
    Back = 2,
    Left = 3,
    Right = 4
};

class BufferCalculator
{
    vec3 cameraPosition = vec3(2.0f, 2.0f, 2.0f);
    time_point<steady_clock> startTime = high_resolution_clock::now();
    int frameCounter = 0;

    time_point<steady_clock> lastTimeCameraMovedForward = high_resolution_clock::now();
    int cameraMovingForward = 0;

    time_point<steady_clock> lastTimeCameraMovedSide = high_resolution_clock::now();
    int cameraMovingSide = 0;

    vec3 currentDirection = vec3(1, 0, 0);

public:
    UniformBufferObject GetUniformBuffer(float mouseXPosition, float mouseYPosition, float aspect)
    {
        auto currentTime = high_resolution_clock::now();
        float seconds = duration<float, seconds::period>(currentTime - startTime).count();

        int fps = frameCounter / seconds;

        UniformBufferObject ubo = {};

        ubo.diffuse = vec4(0.01, 0.01, 0.01, 0.0);
        ubo.spotExponent = vec4(1000.0, 0.0, 0.0, 0.0);
        ubo.diffuseRatio = vec4(0.1, 1.0, 1.0, 1.0);
        ubo.SpecularRatio = vec4(0.01, 1.0, 1.0, 1.0);

        float pitchRotation = mouseYPosition / 1000;
        float yawRotation = mouseXPosition / 1000;

        mat4 matPitch = rotate(mat4(1.0), pitchRotation, vec3(1.0f, 0.0f, 0.0f));
        mat4 matYaw = rotate(mat4(1.0), yawRotation, vec3(0.0f, 1.0f, 0.0f));
        mat4 rotatea = matPitch * matYaw;
        mat4 translation = translate(mat4(1.0f), -cameraPosition);

        ubo.view = rotatea * translation;

        ubo.Lighthouse = ubo.view*vec4(50.0, 50.0, 50.0, 1.0);

        PrepareModelMatrices(ubo.models, fps, pitchRotation, yawRotation);

        if (cameraMovingForward != 0)
        {
            float timeSinceLastMove = duration<float, seconds::period>(currentTime - lastTimeCameraMovedForward).count();
            lastTimeCameraMovedForward = currentTime;
            vec3 forward(ubo.view[0][2], ubo.view[1][2], ubo.view[2][2]);
            cameraPosition += forward*(-timeSinceLastMove*cameraMovingForward * 5);
        }

        if (cameraMovingSide != 0)
        {
            float timeSinceLastMove = duration<float, seconds::period>(currentTime - lastTimeCameraMovedSide).count();
            lastTimeCameraMovedSide = currentTime;
            vec3 strafe(ubo.view[0][0], ubo.view[1][0], ubo.view[2][0]);
            cameraPosition += strafe*(timeSinceLastMove*cameraMovingSide * 5);
        }



        ubo.proj = perspective(radians(45.0f), aspect, 0.1f, 100.0f);
        ubo.proj[1][1] *= -1;

        frameCounter++;

        return ubo;
    }

    void StartMovingCamera(DirectionEnum direction)
    {
        if (direction == Forward)
        {
            lastTimeCameraMovedForward = high_resolution_clock::now();
            cameraMovingForward = 1;
        }

        if (direction == Back)
        {
            lastTimeCameraMovedForward = high_resolution_clock::now();
            cameraMovingForward = -1;
        }

        if (direction == Left)
        {
            lastTimeCameraMovedSide = high_resolution_clock::now();
            cameraMovingSide = -1;
        }

        if (direction == Right)
        {
            lastTimeCameraMovedSide = high_resolution_clock::now();
            cameraMovingSide = 1;
        }
    }

    void StopMovingCamera(DirectionEnum direction)
    {
        if (direction == Forward || direction == Back)
            cameraMovingForward = 0;

        if (direction == Left || direction == Right)
            cameraMovingSide = 0;
    }

    void PrepareModelMatrices(mat4* modelMatrices, int numberToDisplay, float pitchRotation, float yawRotation)
    {
        if (numberToDisplay >= 100000)
        {
            throw runtime_error("Too big of a number!");
        }

        auto farMatrix = translate(mat4(), vec3(1000, 3.0f, 3.0f));

        modelMatrices[0] = mat4();

        for (int i = 1;i < 64;i++)
            modelMatrices[i] = farMatrix;


        modelMatrices[61]= translate(mat4(1.0f), vec3(20.0, 20.0, 20.0));
        mat4 matPitcha = rotate(mat4(1.0), pitchRotation, vec3(1.0f, 0.0f, 0.0f));
        mat4 matYawa = rotate(mat4(1.0), yawRotation, vec3(0.0f, 1.0f, 0.0f));
        mat4 rotatea = matPitcha * matYawa;
        mat4 translation = translate(mat4(1.0f), -cameraPosition);
        mat4 invView = inverse(rotatea * translation);

        mat4 matrix = mat4();

        matrix[0].x = cos(3.3);
        matrix[0].z = -sin(3.3);
        matrix[2].x = sin(3.3);
        matrix[2].z = cos(3.3);

        matrix[3].x = 0.2;
        matrix[3].y = -0.2;
        matrix[3].z = -1;

        modelMatrices[60] = invView*matrix;


        int indexes[10];
        for (int i = 0;i < 10;i++)
            indexes[i] = 5 * i + 1;

        int digitIndex = 0;

        while (numberToDisplay > 0)
        {
            int digit = numberToDisplay % 10;

            int indexInModelArray = indexes[digit];

            vec3 digitPosition = vec3(0.3 - (digitIndex / 50.0), 0.3, -1.0);
            mat4 matRotate = rotate(mat4(1.0), 1.57f - pitchRotation, vec3(1.0f, 0.0f, 0.0f));

            vec3 newAxis = rotateX(vec3(0.0f, 0.0f, 1.0f), pitchRotation);
            mat4 matYaw = rotate(mat4(1.0), yawRotation, newAxis);
            digitPosition = rotateX(digitPosition, -pitchRotation);
            digitPosition = rotateY(digitPosition, -yawRotation);

            mat4 translation = translate(mat4(), digitPosition + cameraPosition);

            modelMatrices[indexInModelArray] = translation * matRotate * matYaw;

            indexes[digit]++;
            digitIndex++;
            numberToDisplay /= 10;
        }
    }
};