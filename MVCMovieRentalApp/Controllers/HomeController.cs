using MVCMovieRentalApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using MVCMovieRentalApp.ViewModels;

namespace MVCMovieRentalApp.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext _context;

        public HomeController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Movies()
        {
            var movies = _context.Movies.Include(m => m.Genre).ToList();
            return View(movies);
        }

        public ActionResult Customers()
        {
            var customers = _context.Customers.Include(c => c.MembershipType).ToList();
            return View(customers);
        }

        public ActionResult CustomerDetails(Customer customer)
        {
            var cust = _context.Customers.Include(c => c.MembershipType).SingleOrDefault(c=> c.Id == customer.Id);
            return View(cust);
        }

        public ActionResult MovieDetails(Movie movie)
        {
            var mov = _context.Movies.Include(m => m.Genre).SingleOrDefault(m => m.Id == movie.Id);
            return View(mov);
        }

        public ActionResult CustomerForm()
        {
            var viewModel = new NewCustomerViewModel
            {
                MembershipTypes = _context.MembershipTypes.ToList()
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult AddCustomer(Customer customer)
        {
            //if(!ModelState.IsValid)
            //{
            //    var viewModel = new NewCustomerViewModel
            //    {
            //        Customer = customer,
            //        MembershipTypes = _context.MembershipTypes.ToList()
            //    };
            //    return View("CustomerForm",viewModel);
            //}

            _context.Customers.Add(customer);
            _context.SaveChanges();
            return RedirectToAction("CustomerDetails", customer);
        }

        public ActionResult CustEditForm(Customer customer)
        {
            if (customer == null)
                return HttpNotFound();
            var viewModel = new NewCustomerViewModel
            {
                Customer = customer,
                MembershipTypes = _context.MembershipTypes.ToList()
            };
            return View(viewModel);
        }

        public ActionResult EditCustomer(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new NewCustomerViewModel
                {
                    Customer = customer,
                    MembershipTypes = _context.MembershipTypes.ToList()
                };
                return View("CustEditForm", viewModel);
            }

            var customerInDb = _context.Customers.Single(c => c.Id == customer.Id);
            customerInDb.Name = customer.Name;
            customerInDb.DateOfBirth = customer.DateOfBirth;
            customerInDb.MembershipTypeId = customer.MembershipTypeId;
            customerInDb.IsSubscribedToNewsLetter = customer.IsSubscribedToNewsLetter;
            _context.SaveChanges();
            return RedirectToAction("CustomerDetails", customer);
        }
    }
}