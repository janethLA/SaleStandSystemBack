using Microsoft.EntityFrameworkCore;
using MySalesStandSystem.Data;
using MySalesStandSystem.Models;
using MySalesStandSystem.Output;

namespace MySalesStandSystem.Repository
{
    public class SalesStandRepository: ISalesStandRepository
    {
        protected readonly ApplicationDbContext _context;
        protected UserRepository _userRepository;
        public SalesStandRepository( ApplicationDbContext context) {
            _context = context;
           // _userRepository = userRepository;
        } 

        public IEnumerable<SalesStand> GetSalesStands()
        {
            return _context.salesStands.Include(c => c.products).ToList();
        }

        public SalesStand GetSalesStandById(int id)
        {
            //return _context.categories.Find(id);
            var salesStand = _context.salesStands.Where(c => c.id == id).Include(c => c.products).First();
            return salesStand;
        }
        public async Task<SalesStand> CreateSalesStandAsync(SalesStand salesStand)
        {
            await _context.salesStands.AddAsync(salesStand);
            //await _context.Set<Cow>().AddAsync(cow);
            await _context.SaveChangesAsync();
            return salesStand;
        }

        public async Task<bool> UpdateSalesStandAsync(SalesStand salesStand)
        {
            _context.Entry(salesStand).State = EntityState.Modified;
            //_context.categories.Update(category);
            _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateSalesStandAsync(int id, SalesStand salesStand, IFormFile? image)
        {
            var c = GetSalesStandById(id);
            if (image !=null) {
                using (var ms = new MemoryStream())
                {
                    image.CopyTo(ms);
                    byte[] fileBytes = ms.ToArray();
                    string s = Convert.ToBase64String(fileBytes);
                    salesStand.image = fileBytes;
                    c.image = fileBytes;
                    // act on the Base64 data
                }
            }
           
            if (c == null)
            {
                return false;
            }
            c.salesStandName = salesStand.salesStandName;
            c.description = salesStand.description;
            c.latitude = salesStand.latitude;
            c.longitude = salesStand.longitude;
            c.address = salesStand.address;
           
            _context.SaveChanges();
            return true;
        }

        public async Task<bool> DeleteSalesStandAsync(SalesStand salesStand)
        {
            //var entity = await GetByIdAsync(id);
            if (salesStand is null)
            {
                return false;
            }
            _context.Set<SalesStand>().Remove(salesStand);
            await _context.SaveChangesAsync();

            return true;
        }

        public List<SalesStandOutput> GetAllSalesStands()
        {
            List<SalesStandOutput> salesO = new List<SalesStandOutput>();
            IEnumerable<SalesStand> listFind = GetSalesStands().Reverse();
            foreach (SalesStand sale in listFind)
            {
                SalesStandOutput s = new SalesStandOutput();
                s.id = sale.id;
                s.salesStandName = sale.salesStandName;
                s.description = sale.description;
                s.image = sale.image;
                s.address = sale.address;
                salesO.Add(s);
            }
            return salesO;
        }
    }
}
