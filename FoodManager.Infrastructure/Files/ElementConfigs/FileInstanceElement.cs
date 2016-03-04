using System.Configuration;

namespace FoodManager.Infrastructure.Files.ElementConfigs
{
    public class FileInstanceElement : ConfigurationElement
    {
        [ConfigurationProperty("name", IsKey = true, IsRequired = true)]
        public string Name
        {
            get { return (string) base["name"]; }
            set { base["name"] = value; }
        }

        [ConfigurationProperty("", IsRequired = true, IsDefaultCollection = true)]
        [ConfigurationCollection(typeof (HexInstanceCollection), AddItemName = "signature", ClearItemsName = "clear", RemoveItemName = "remove")]
        public HexInstanceCollection Signatures
        {
            get { return (HexInstanceCollection) this[""]; }
            set { this[""] = value; }
        }
    }
}