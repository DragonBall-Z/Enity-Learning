#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DemoApi.Db;
using DemoApi.Models;

namespace DemoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly DemoDbContext _context;

        public UsersController(DemoDbContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = _context.Users
                .Include(zz => zz.PhotoList)
                .Include(zz => zz.UserAddres)
                .Include(zz => zz.BankAccounts)
                .ToList();

            Console.WriteLine(users.Count);
          
            return users;
          //  return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(Guid id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(Guid id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser()
        {
            //var user = new User();
            //user.Id = Guid.NewGuid();
            //user.FirstName = "Vishnu";
            //user.LastName = "Kumar PS";
            //user.LastName2 = "ps1";

            //user.UserAddres = new UserAddress() { 
            //  Id = Guid.NewGuid(),
            //  UserId= user.Id,//fk
            //  AddressLine1="Kollam",
            //  AddressLine2="Alappuzha",
              
            //};
            //user.BankAccounts = new List<BankAccount>() {
            // new BankAccount() { Id=Guid.NewGuid(),UserId=user.Id,BankName="Sydicate"},
            //  new BankAccount() { Id=Guid.NewGuid(),UserId=user.Id,BankName="Central"},
            // new BankAccount() { Id=Guid.NewGuid(),UserId=user.Id,BankName="HDFC"},
            //};

            //user.PhotoList = new List<Photo>() {
            //  new Photo() { Id=Guid.NewGuid(),UserId=user.Id,PhotUrl="Url1"},
            //  new Photo() { Id=Guid.NewGuid(),UserId=user.Id,PhotUrl="Url2"},
            //  new Photo() { Id=Guid.NewGuid(),UserId=user.Id,PhotUrl="Url3"}
            //};





            

     



            //_context.Users.Add(user);
            //await _context.SaveChangesAsync();
            /////////////////////////////////////////////

            var book=new Book();
            book.Title = "BookName 101";
           
            _context.Books.Add(book);
            _context.SaveChanges();

            var author= new Author();
            author.Id = new Guid();
            author.PoeticName = "VK";
            author.AuthorName = "Vishnu Kumar PS";

            _context.Authors.Add(author);
            _context.SaveChanges();

            var book_author = new Book_Author();
            book_author.BookId=book.Id;
            book_author.AuthorId=author.Id;

            _context.Books_Authors.Add(book_author);
            _context.SaveChanges();

            var result= _context.Books_Authors
                .Include(x => x.Book)
                .Where(entry => entry.AuthorId.ToString() == "80d4ed91-8dc7-49a2-7772-08d9fec93a50").Select(entry => entry.Book);


            return Ok(result);
        }

        [HttpPost]
        [Route("PostUser2")]
        public async Task<ActionResult<User>> PostUser2(User user)
        {
         
            _context.Users.Add(user);
           
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(Guid id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
