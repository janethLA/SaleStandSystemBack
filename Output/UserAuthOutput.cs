namespace MySalesStandSystem.Output
{
    public class UserAuthOutput
    {
        public UserAuthOutput(string token,int idUser, string name, string rol) {
            this.token = token;
            this.idUser = idUser;
            this.name = name;
            this.rol = rol;
        }
        public string token { get; set; }
        public int idUser { get; set; }
        public string name { get; set; }
        public string rol { get; set; }
    }
}
