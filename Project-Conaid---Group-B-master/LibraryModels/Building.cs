namespace LibraryModels
{
    public class Building
    {
        public Building(int id, string filePath)
        {
            Id = id;
            FilePath = filePath;
        }

        public string FilePath { get; set; }
        public int Id { get; set; }

        public override string ToString()
        {
            return Id + "    " + FilePath;
        }
    }
}