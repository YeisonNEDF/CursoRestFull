using Core.Common;

namespace Core.Domain.Entities
{
    public class Cliente : AuditableBaseEntity
    {
        private int _edad;

        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Direccion { get; set; }

        //Obtendremos la edad calculada referente a lo que nos envian desde el modelo con FechaNacimiento
        public int Edad
        {
            get
            {
                if (this._edad <= 0)
                {
                    this._edad = new DateTime(DateTime.Now.Subtract(this.FechaNacimiento).Ticks).Year - 1;
                }
                return this._edad;
            }
            set
            {
                this._edad = value;
            }
        }
    }
}
