namespace ProgramPLC.DTOs
{
    public class DeviceCreateDTO
    {
        public string DeviceName { get; set; } = null!;

        public string DeviceValue { get; set; } = null!;

        public int EngineId { get; set; }
    }
}
