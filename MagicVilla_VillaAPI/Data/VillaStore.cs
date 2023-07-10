using MagicVilla_VillaAPI.Models.DTO;

namespace MagicVilla_VillaAPI.Data
{
    public static class VillaStore
    {
        public static List<VillaDTO> villaList = new ()
        {
            new VillaDTO {Id = 1, Name = "poool_view"},
            new VillaDTO {Id = 2, Name = "beach_view"}
        };
    }
}
