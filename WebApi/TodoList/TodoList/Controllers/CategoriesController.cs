using System.Linq;
using System.Net;
using System.Web.Http;
using TodoList.Data;
using TodoList.Models;

namespace TodoList.Controllers
{
    public class CategoriesController : ApiController
    {
        private readonly TodoDbContext db = new TodoDbContext();

        public IQueryable<Categorie> GetCategories()
        {
            return this.db.Categories.OrderBy(x => x.Nom);
        }

        public IHttpActionResult GetCategorie(int id)
        {
            var categorie = this.db.Categories.Find(id);
            if (categorie == null)
                return this.NotFound();
            return this.Ok(categorie);
        }

        public IHttpActionResult PostCategories(Categorie categorie)
        {
            if (this.ModelState.IsValid)
            {
                this.db.Categories.Add(categorie);
                this.db.SaveChanges();
                return this.Ok(categorie);
            }
            else
                return this.BadRequest(this.ModelState);
        }

        public IHttpActionResult PutCategories(int id, Categorie categorie)
        {
            if (id != categorie.ID)
                return this.BadRequest();

            if (!this.ModelState.IsValid)
                return this.BadRequest(this.ModelState);

            if (this.db.Categories.Count(x => x.ID == id) != 1)
                return this.BadRequest();

            this.db.Entry(categorie).State = System.Data.Entity.EntityState.Modified;
            this.db.SaveChanges();

            return this.StatusCode(HttpStatusCode.NoContent);
        }

        public IHttpActionResult DeleteCategories(int id)
        {
            var categorie = this.db.Categories.Find(id);
            if (categorie == null)
                return this.NotFound();

            this.db.Entry(categorie).State = System.Data.Entity.EntityState.Deleted;
            this.db.SaveChanges();

            return this.Ok(categorie);
        }


        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            this.db.Dispose();
        }
    }
}
