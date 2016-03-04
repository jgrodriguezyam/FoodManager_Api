using System.Configuration;

namespace FoodManager.Infrastructure.Files.ElementConfigs
{
    public class FileConfigSection : ConfigurationSection
    {
        [ConfigurationProperty("", IsRequired = true, IsDefaultCollection = true)]
        [ConfigurationCollection(typeof (FileInstanceCollection), AddItemName = "extension", ClearItemsName = "clear",
            RemoveItemName = "remove")]
        public FileInstanceCollection Extensions
        {
            get { return (FileInstanceCollection) this[""]; }
            set { this[""] = value; }
        }
    }
}