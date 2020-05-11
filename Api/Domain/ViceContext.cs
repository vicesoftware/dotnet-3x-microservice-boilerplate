using Api.Domain.Documents;
using Microsoft.EntityFrameworkCore;

namespace Api.Domain
{
    public class ViceContext :  DbContext
   {
       public DbSet<Document> Documents { get; set; }
       
       public ViceContext()
       {
           
       }

       public ViceContext(DbContextOptions<ViceContext> context) : base(context)
       {
           
       }
   }
}