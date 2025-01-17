﻿using TollCalculator.Helpers;

namespace TollCalculator.Models;

public class DayTimeFee
{
    public TimeOnly Start { get; }
    public TimeOnly End { get; }
    public decimal Fee { get; }

    private DayTimeFee(TimeOnly start, TimeOnly end, decimal fee)
    {
        Start = start;
        End = end;
        Fee = fee;
    }

    /// <summary>
    /// Creates a normal DayTimeFee.
    /// </summary>
    /// <param name="start">Start time.</param>
    /// <param name="end">End time</param>
    /// <param name="fee">Fee.</param>
    /// <returns>An instance of DayTimeFee.</returns>
    /// <exception cref="ArgumentException"></exception>
    public static DayTimeFee Create(TimeOnly start, TimeOnly end, decimal fee)
    {
        ArgumentNullException.ThrowIfNull(start, nameof(start));
        ArgumentNullException.ThrowIfNull(end, nameof(end));
        if (start > end)
        {
            throw new ArgumentException($"The {nameof(start)} is greater than {nameof(end)}.");
        }

        if (fee < Constants.MinimumFeeDependingOnTheTimeOfDay || fee > Constants.MaximumFeeDependingOnTheTimeOfDay)
        {
            throw new ArgumentException(
                $"The {nameof(fee)} must be between {Constants.MinimumFeeDependingOnTheTimeOfDay} and {Constants.MaximumFeeDependingOnTheTimeOfDay}.");
        }

        return new DayTimeFee(start, end, fee);
    }

    /// <summary>
    /// Creates a rush hour DayTimeFee.
    /// </summary>
    /// <param name="start">Start time.</param>
    /// <param name="end">End time</param>
    /// <returns>An instance of DayTimeFee.</returns>
    public static DayTimeFee CreateRushHour(TimeOnly start, TimeOnly end)
    {
        return Create(start, end, Constants.MaximumFeeDependingOnTheTimeOfDay);
    }
}