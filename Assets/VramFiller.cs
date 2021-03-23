using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using Random = System.Random;

public class VramFiller
{
    public static IEnumerable<Texture3D> Allocate3DTextures(float gigabytes)
    {
        // Spawn GB chunks
        var chunks = Mathf.Floor(gigabytes);
        for (var i = 0; i < chunks; i++)
        {
            yield return SpawnCubeTexture(1024);
        }

        // Spawn rest
        var gb = 1024 * 1024 * 1024;
        var restBytes = (gigabytes - (int) gigabytes) * gb;
        var cubeRoot = (int) Mathf.Pow(restBytes, 1f / 3f);
        if (cubeRoot > 0)
        {
            yield return SpawnCubeTexture(cubeRoot);
        }
    }

    private static Texture3D SpawnCubeTexture(int sideLength)
    {
        var OneBytePerPixelFormat = new[]
            {
                GraphicsFormat.R8_SInt, 
                GraphicsFormat.R8_SNorm, 
                GraphicsFormat.R8_UInt,
                GraphicsFormat.R8_UNorm
            }
            .FirstOrDefault(f => SystemInfo.IsFormatSupported(f, FormatUsage.Sample));

        var spawnCubeTexture = new Texture3D(sideLength, sideLength, sideLength, OneBytePerPixelFormat, TextureCreationFlags.None);

        var l = sideLength * sideLength * sideLength;
        var data = new byte[l];
        for (var i = 0; i < data.Length; i++)
        {
            data[i] = (byte) i;
        }
        spawnCubeTexture.SetPixelData(data, 0);
        spawnCubeTexture.Apply();
        return spawnCubeTexture;
    }
}