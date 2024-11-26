// � 2024 Irlen Turlykhanov & CO (Almaty Guys Studio). All rights reserved.
// Our studio is leveraging AI to become a Triple-A game studio.
// Telegram: @cheburek_kerubech
// GitHub: irlen-dot

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Collections;
using UnityEngine;
using System.Linq;
using System;

public class InterpolationBetween
{

    public float CurrValue { get; }
    public float PrevValue { get; }


    public static List<float> GetEvenlyDistributedNumbersAsArray(float start, float end, int count)
    {
        // Ensure start is the smaller number and end is the larger number
        if (start > end)
        {
            float temp = start;
            start = end;
            end = temp;
        }

        if (count <= 1)
        {
            return new List<float> { start };
        }

        List<float> numbers = new List<float>();
        float interval = (end - start) / (count - 1);

        for (int i = 0; i < count; i++)
        {
            float number = start + i * interval;
            numbers.Add(number);
        }

        return numbers;
    }
}
