namespace TeamDevelopmentBackend.Model
{
    public class BuildingDbModel
    {
        public Guid Id { get; set; } 
        public string Name { get; set; }
        public ICollection<RoomDbModel> Rooms { get; set; }
    }
}
