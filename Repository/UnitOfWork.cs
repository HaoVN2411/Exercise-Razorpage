using Repository.Entites;

namespace Repository
{
    public class UnitOfWork : IDisposable
    {
        private Eyeglasses2024DBContext context = new Eyeglasses2024DBContext();
        private GenericRepository<StoreAccount> storeAccount;
        private GenericRepository<Eyeglass> eyeGlass;
        private GenericRepository<LensType> lensType;

        public GenericRepository<StoreAccount> StoreAccountRepository
        {
            get
            {
                if (this.storeAccount == null)
                {
                    this.storeAccount = new GenericRepository<StoreAccount>(context);
                }
                return storeAccount;
            }
        }
        public GenericRepository<LensType> LensTypeRepository
        {
            get
            {
                if (this.lensType == null)
                {
                    this.lensType = new GenericRepository<LensType>(context);
                }
                return lensType;
            }
        }
        public GenericRepository<Eyeglass> EyeGlassRepository
        {
            get
            {
                if (this.eyeGlass == null)
                {
                    this.eyeGlass = new GenericRepository<Eyeglass>(context);
                }
                return eyeGlass;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}