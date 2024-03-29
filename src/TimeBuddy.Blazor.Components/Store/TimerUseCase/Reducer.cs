﻿using Fluxor;

namespace TimeBuddy.Blazor.Components.Store.TimerUseCase;

public static class Reducer
{
    [ReducerMethod]
    public static TimerState ReduceSetActivityAction(TimerState state, SetActivityAction action) =>
        state with
        {
            Activity = action.Activity
        };

    [ReducerMethod]
    public static TimerState ReduceSetLastStartAction(TimerState state, SetLastStartAction action) =>
        state with
        {
            LastStart = action.StartedAt
        };
    
    [ReducerMethod]
    public static TimerState ReduceStopAction(TimerState state, AddRecordedTimeFrame action)
    {
        var recordedFrames = state.RecordTimeFrames.ToList();
        recordedFrames.Add(action.TimeFrame);
        return state with
        {
            RecordTimeFrames = recordedFrames
        };
    }

    [ReducerMethod]
    public static TimerState ReduceRefreshUiTimerAction(TimerState state, RefreshUiTimerAction action) =>
        state with
        {
            ElapsedTime = DateTime.UtcNow - state.LastStart.ToUniversalTime()
        };

    [ReducerMethod]
    public static TimerState ReduceSetLoadedStateAction(TimerState _, SetLoadedStateAction action) => action.State;

    [ReducerMethod]
    public static TimerState ReduceSetActiveProjectAction(TimerState state, SetActiveProjectAction action) =>
        state with
        {
            ActiveProject = action.Project
        };
    
    [ReducerMethod]
    public static TimerState ReduceSetActiveProjectDetailsAction(TimerState state, SetActiveProjectDetailsAction action) =>
        state with
        {
            ActiveProjectDetails = action.ProjectDetails
        };
}