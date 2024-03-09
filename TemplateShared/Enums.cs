using System;

namespace TemplateShared
{
    [Flags]
    public enum UserModelStatus
    {
        OK = 0,
        DuplicateUsername = 1 << 0,
        DuplicateEmail = 1 << 1,
        UserNotExists = 1 << 2,
        MailQueueError = 1 << 3,
        EmptyUsername = 1 << 6,
        InvalidUsername = 1 << 7,
        EmptyEmail = 1 << 8,
        InvalidEmail = 1 << 9,
        EmptyPassword = 1 << 10,
        InvalidPassword = 1 << 11,
        PasswordsNotMatch = 1 << 12,
        InvalidBirthday = 1 << 13,
        TermsNotAgreed = 1 << 14,
        AlreadyConfirmed = 1 << 15,
        WrongPassword = 1 << 16,
        EmptyOldPassword = 1 << 17,
        InvalidAddress = 1 << 18
    }

    [Flags]
    public enum AddressModelStatus
    {
        OK = 0,
        EmptyStreet = 1 << 0,
        EmptyBuilding = 1 << 1,
        EmptyZip = 1 << 2,
        EmptyCity = 1 << 3,
        EmptyCountry = 1 << 4,
        WrongBuildingFormat = 1 << 5,
        WrongZipFormat = 1 << 6
    }
}
