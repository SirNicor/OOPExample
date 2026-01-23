using System.ComponentModel;

namespace UCore;

public enum UserStateRegEnum
{
    [Description("notInitialized")]
    notInitialized,
    [Description("forRegistration")]
    forRegistration,
    [Description("waitingForUniversityInput")]
    waitingForUniversityInput,
    [Description("waitingForFacultyInput")]
    waitingForFacultyInput,
    [Description("waitingForDepartmentInput")]
    waitingForDepartmentInput,
    [Description("waitingForDirectionInput")]
    waitingForDirectionInput,
    [Description("waitingForLastNameInput")]
    waitingForLastNameInput,
    [Description("waitingForFirstNameInput")]
    waitingForFirstNameInput,
    [Description("fullRegistration")]
    fullRegistration
}

public enum UserStateTypeRequest
{
    [Description("Send discipline")]
    DisciplineUpdate,
    [Description("Send Schedule")]
    ScheduleUpdate,
    [Description("Send Student")]
    StudentUpdate
}