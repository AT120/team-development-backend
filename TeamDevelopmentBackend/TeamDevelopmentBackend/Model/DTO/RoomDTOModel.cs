namespace TeamDevelopmentBackend.Model.DTO
{
    public class RoomDTOModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public RoomDTOModel(RoomDbModel room)
        {
            Id = room.Id;
            Name = room.Name;
        }
    }
}
