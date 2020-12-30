namespace RedmineHelper.Models.Dto
{
    using Abstractions;
    public class RedmineTaskDto : BaseDto
    {
        /// <summary>
        /// Тема
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; }
    }
}
