using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizApi.Models
{
    public class Question
    {
        [Key]
        public int QnId { get; set; }


        [Column(TypeName="nvarchar(250)")]
        public string QnInWords {  get; set; }

        [Column(TypeName = "nvarchar(50)")]

        public string? ImageName { get; set; }

        [Column(TypeName = "nvarchar(50)")]

        public string Optionl1 { get; set; }

        [Column(TypeName = "nvarchar(50)")]


        public string Optionl2 { get; set; }

        [Column(TypeName = "nvarchar(50)")]


        public string Optionl3 { get; set; }

        [Column(TypeName = "nvarchar(50)")]

        public string Optionl4 { get; set; }

        public int Answer { get; set; }
    }
}
