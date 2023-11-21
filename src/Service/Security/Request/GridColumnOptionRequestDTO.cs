using System.Collections.Generic;

namespace Portolo.Security.Request
{
    public class GridColumnOptionRequestDTO
    {
        public GridColumnOptionRequestDTO()
        {
            GridColumnList = new List<GridColumnSequenceDTO>();
        }
        public int UserId { get; set; }     
       
        public string GridName { get; set; }
        public List<GridColumnSequenceDTO> GridColumnList { get; set; }
    }
    public class GridColumnSequenceDTO
    {
        public string FieldReferenceName { get; set; }
        public string FieldReferenceValue { get; set; }
    }
}
