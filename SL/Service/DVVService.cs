using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Contracts;
using Domain;
using SL.Composite;
using SL.Domain;
using SL.Factory;
using SL.Helpers;

namespace SL.Services
{
    public class DVVService
    {
        private IGenericRepository<User> _userRepo;
        private IGenericRepository<Booking> _reservaRepo;
        private IGenericRepository<Field> _canchaRepo;
        private IGenericRepository<Log> _logRepo;
        private IGenericRepository<PermissionComponent> _permisoRepo;

        public void RecalcularDVV<T>(IEnumerable<T> lista, string tabla) where T : IDVH
        {
            string concatenado = string.Join("", lista.Select(e => e.DVH));
            string nuevoDVV = HashHelper.CalculateSHA256(concatenado);
            var dvvRepo = SLFactory.Current.GetVerificadorVerticalRepository();
            dvvRepo.SetDVV(tabla, nuevoDVV);
        }

        public void UpdateDVV(string tabla, string nuevoDVV)
        {
            var dvvRepo = SLFactory.Current.GetVerificadorVerticalRepository();
            dvvRepo.SetDVV(tabla, nuevoDVV);
        }

        public bool VerificarIntegridadCompleta()
        {
            bool todoOk = true;

            todoOk &= VerificarTabla<User>(_userRepo, "Usuarios");
            todoOk &= VerificarTabla<Booking>(_reservaRepo, "Reservas");
            todoOk &= VerificarTabla<Field>(_canchaRepo, "Canchas");
            todoOk &= VerificarTabla<PermissionComponent>(_permisoRepo, "Permisos");

            return todoOk;
        }

        private bool VerificarTabla<T>(IGenericRepository<T> repo, string tabla) where T : IDVH
        {
            var lista = repo.GetAll();

            string concatenado = string.Join("", lista.Select(e => e.DVH));
            string dvvCalculado = HashHelper.CalculateSHA256(concatenado);

            var dvvRepo = SLFactory.Current.GetVerificadorVerticalRepository();
            string dvvAlmacenado = dvvRepo.GetDVV(tabla);

            return dvvCalculado == dvvAlmacenado;
        }
    }
}
