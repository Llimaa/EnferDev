namespace EnferDev.Api.ViewModels
{
    public class HospitalViewModel
    {
        public string Id { get; set; }
        public string IdAddress { get; set; }
        public string Name { get; set; }
        public string CNPJ { get; set; }
        public bool Active { get; set; }

        public string Street { get; set; }
        public string Number { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
    }
}
