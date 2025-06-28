namespace Applications.DTO.Create
{
    public class UserCreateDto
    {
        public string FullName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public Guid MajorID { get; set; }
        public string Role { get; set; }
        public string Code { get; set; }  
        public int Status { get; set; }
    }
}
