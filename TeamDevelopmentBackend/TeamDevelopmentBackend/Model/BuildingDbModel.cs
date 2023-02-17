namespace TeamDevelopmentBackend.Model
{
    public class BuildingDbModel
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public RoomDbModel[] Rooms { get; set; }
    }
}
