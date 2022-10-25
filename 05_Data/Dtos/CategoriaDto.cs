using _05_Data.Data;

namespace _05_Data.Dtos
{
    public class CategoriaDto
    {       
        //Constructor que recibe un Objeto entity
        public CategoriaDto(Categoria entity)
        {
            if (entity == null) return;
            CategoryID = entity.CategoryID;
            CategoryName = entity.CategoryName;
            Description = entity.Description;
        }
        //Constructor que recibe un Objeto vacío
        public CategoriaDto()
        {
        }
        //Atributos del Dto que se llaman igual que los de la Entity
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
    }
}
