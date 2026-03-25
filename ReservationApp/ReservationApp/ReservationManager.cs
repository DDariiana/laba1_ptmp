using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class ReservationManager
{
    public List<Reservation> Reservations { get; private set; }

    public ReservationManager()
    {
        Reservations = new List<Reservation>();
        LoadReservations();
    }

    public void AddReservation(Reservation reservation)
    {
        if (reservation == null)
        {
            throw new ArgumentNullException(nameof(reservation));
        }
        Reservations.Add(reservation);
        SaveReservations();
    }

    public void RemoveReservation(Reservation reservation)
    {
        if (reservation == null)
        {
            throw new ArgumentNullException(nameof(reservation));
        }
        Reservations.Remove(reservation);
        SaveReservations();
    }

    public void UpdateReservationStatus(Reservation reservation, ReservationStatus newStatus)
    {
        if (reservation == null)
        {
            throw new ArgumentNullException(nameof(reservation));
        }
        reservation.UpdateStatus(newStatus);
        SaveReservations();
    }

    private void SaveReservations()
    {
        // var lines = Reservations.Select(r =>
        //    $"{r.CustomerName}|{r.StartTime:yyyy-MM-dd HH:mm}|{r.EndTime:yyyy-MM-dd HH:mm}|{(int)r.Status}");
        // File.WriteAllLines("reservations.txt", lines);
        File.WriteAllLines("reservations.txt", Reservations.Select(r =>
        $"{r.CustomerName}|{r.StartTime.ToString("yyyy-MM-dd HH: mm")}|{r.EndTime.ToString("yyyy - MM - dd HH: mm")}|{(int)r.Status}"));
    }

    private void LoadReservations()
    {
        // if (!File.Exists("reservations.txt")) return;

        // var lines = File.ReadAllLines("reservations.txt");
        // foreach (var line in lines)
        // {
        //    var parts = line.Split('|');
        //    if (parts.Length == 4)
        //    {
        //        if (DateTime.TryParse(parts[1], out DateTime startTime) &&
        //            DateTime.TryParse(parts[2], out DateTime endTime) &&
        //            Enum.TryParse<ReservationStatus>(parts[3], out ReservationStatus status))
        //        {
        //            var reservation = new Reservation(parts[0], startTime, endTime);
        //            reservation.Status = status;
        //            Reservations.Add(reservation);
        //        }
        //    }
        // }

        if (File.Exists("reservations.txt"))
        {
            var lines = File.ReadAllLines("reservations.txt");
            foreach (var line in lines)
            {
                var parts = line.Split('|');
                if (parts.Length == 4)
                {
                    DateTime startTime;
                    DateTime endTime;
                    ReservationStatus status;
                    if (DateTime.TryParse(parts[1], out startTime) && DateTime.TryParse(parts[2],
                    out endTime) && Enum.TryParse<ReservationStatus>(parts[3], out status))
                    {
                        Reservations.Add(new Reservation(parts[0], startTime, endTime));
                        Reservations.Last().Status = status;
                    }
                }
            }
        }
    }
}