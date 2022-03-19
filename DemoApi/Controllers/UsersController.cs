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

        [HttpPost("Random")]
        public List<Book> RandomQuery(string name)
        {
            var students = _context.Books
                  .FromSqlRaw($"Select * from Books")
                  .ToList();

            string sql = "EXEC sp_getBookByName";
           // var list = _context.Books.FromSqlRaw<Book>(sql).ToList();

            var books = _context.Books
                      .FromSqlRaw($"sp_getBookByName {name}")
                      .ToList();


            return books;

        }

        // GET: api/Users
        [HttpGet("GetBOOKS")]
        public async Task<List<Book>> GetBOOKS()
        {

            var result2 = _context.Books.Include(x => x.Book_Authors)
                                      .ThenInclude(x => x.Author)
                                      .ToList();

            //var users = _context.Users
            //    .Include(zz => zz.PhotoList)
            //    .Include(zz => zz.UserAddres)
            //    .Include(zz => zz.BankAccounts)
            //    .ToList();

           // Console.WriteLine(users.Count);
          
            return result2;
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
        public async Task<ActionResult<User>> PostUser(User user)
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

            try
            {
                var book = new Book();
                book.Title = "BookName 101";

                _context.Books.Add(book);
                _context.SaveChanges();

                var author = new Author();
                author.Id = new Guid();
                author.PoeticName = "VK";
                author.AuthorName = "Vishnu Kumar PS";

                _context.Authors.Add(author);
                _context.SaveChanges();

                var book_author = new Book_Author();
                book_author.BookId = book.Id;
                book_author.AuthorId = author.Id;

                _context.Books_Authors.Add(book_author);



                var result2 = _context.Books.Include(x => x.Book_Authors)
                                          .ThenInclude(x => x.Author)
                                          .Single(x => x.Id.ToString() == "B7068538-D425-4A18-8A6D-A5990982DF47");

                return Ok(result2);
            }
            catch ( Exception ex)
            {

                throw;
            }
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
