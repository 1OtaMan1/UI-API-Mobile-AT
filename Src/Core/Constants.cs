namespace Core;

public static class Constants
{
    public static class TestCategories
    {
        public const string SmokeUi = "SmokeUI";
        public const string SmokeApi = "SmokeAPI";
        public const string EmailNotification = "EmailNotification";
        public const string UiWithBugs = "UIWithBugs";

        /// <summary>
        /// Run only with turned on Captcha
        /// </summary>
        public const string CaptchaEnabled = "CaptchaEnabled";
    }

    public static class Sizes
    {
        public const int ProperFileSize = 10000;
        public const int NameMaxLength = 255;
        public const int NameMinLength = 3;
        public const int NormalNameLength = 100;
        public const int TooLongNameLength = 256;
        public const int TooShortNameLength = 2;
        public const int TooLongDescriptionLength = 10001;
        public const int SummaryMaxLength = 3993;
        public const int TooLongSummaryLength = 4001;
        public const int OneCharacter = 1;
        public const int OneItem = OneCharacter;
        public const int TwoItems = 2;
        public const int ThreeItems = 3;
        public const int RepeatActionTimes = 4;
        public const int MaxAttachmentsCount = 25;
        public const int WebJobNeededPause = 1500;
    }
}