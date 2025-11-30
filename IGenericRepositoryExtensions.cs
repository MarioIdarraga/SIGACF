using System;

namespace DAL
{
    public static class IGenericRepositoryExtensions
    {
        /// <summary>
        /// Updates the state of a booking.
        /// </summary>
        /// <param name="repository">The booking repository.</param>
        /// <param name="idBooking">The ID of the booking to update.</param>
        /// <param name="newState">The new state to set.</param>
        public static void UpdateState(this IGenericRepository<Booking> repository, Guid idBooking, int newState)
        {
            var booking = repository.GetOne(idBooking);
            if (booking == null)
            {
                throw new InvalidOperationException("Booking not found.");
            }

            booking.State = newState;
            repository.Update(idBooking, booking);
        }
    }
}
