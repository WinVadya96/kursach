using System.Collections.Generic;

namespace kursach.Models
{
    public class Collection
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string String1Name { get; set; }
        public bool String1Visible { get; set; }
        public string String2Name { get; set; }
        public bool String2Visible { get; set; }
        public string String3Name { get; set; }
        public bool String3Visible { get; set; }
        public string Number1Name { get; set; }
        public bool Number1Visible { get; set; }
        public string Number2Name { get; set; }
        public bool Number2Visible { get; set; }
        public string Number3Name { get; set; }
        public bool Number3Visible { get; set; }
        public string Date1Name { get; set; }
        public bool Date1Visible { get; set; }
        public string Date2Name { get; set; }
        public bool Date2Visible { get; set; }
        public string Date3Name { get; set; }
        public bool Date3Visible { get; set; }
        public string Checkbox1Name { get; set; }
        public bool CheckBox1Visible { get; set; }
        public string Checkbox2Name { get; set; }
        public bool CheckBox2Visible { get; set; }
        public string Checkbox3Name { get; set; }
        public bool CheckBox3Visible { get; set; }
        public string MarkdownField1Name { get; set; }
        public bool MarkdownField1Visible { get; set; }
        public string MarkdownField2Name { get; set; }
        public bool MarkdownField2Visible { get; set; }
        public string MarkdownField3Name { get; set; }
        public bool MarkdownField3Visible { get; set; }



        public int CollectonTopicId { get; set; }
        public virtual CollectionTopic CollectonTopic { get; set; }

        public virtual ICollection<UserCollection> UserCollections { get; set; }
        public virtual ICollection<CollectionTopic> CollectonTopics { get; set; }
    }
}