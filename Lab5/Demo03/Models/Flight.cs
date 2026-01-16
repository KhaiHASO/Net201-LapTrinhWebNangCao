using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demo03.Models
{
    /// <summary>
    /// Entity đại diện cho Chuyến bay
    /// Minh họa: Một entity có 2 Foreign Key trỏ đến cùng 1 entity khác (Airport)
    /// </summary>
    [Table("Flights")]
    public class Flight
    {
        [Key]
        public int FlightId { get; set; }

        [Required(ErrorMessage = "Số hiệu chuyến bay là bắt buộc")]
        [StringLength(20, ErrorMessage = "Số hiệu chuyến bay không được vượt quá 20 ký tự")]
        [Display(Name = "Số hiệu chuyến bay")]
        public string FlightNumber { get; set; } = string.Empty;

        [DataType(DataType.DateTime)]
        [Display(Name = "Thời gian khởi hành")]
        public DateTime? DepartureTime { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Thời gian đến")]
        public DateTime? ArrivalTime { get; set; }

        [Range(0, 1000, ErrorMessage = "Số ghế phải từ 0 đến 1000")]
        [Display(Name = "Số ghế")]
        public int? TotalSeats { get; set; }

        [StringLength(50)]
        [Display(Name = "Trạng thái")]
        public string? Status { get; set; } // "Scheduled", "Delayed", "Cancelled", "Completed"

        // ========================================
        // FOREIGN KEY 1: SÂN BAY ĐI
        // ========================================

        [Required(ErrorMessage = "Vui lòng chọn sân bay đi")]
        [Display(Name = "Sân bay đi")]
        [ForeignKey("DepartureAirport")]
        public int DepartureAirportId { get; set; }

        /// <summary>
        /// Navigation Property - Sân bay khởi hành
        /// Tương ứng với DepartingFlights bên Airport (thông qua [InverseProperty])
        /// </summary>
        public virtual Airport? DepartureAirport { get; set; }

        // ========================================
        // FOREIGN KEY 2: SÂN BAY ĐẾN
        // ========================================

        [Required(ErrorMessage = "Vui lòng chọn sân bay đến")]
        [Display(Name = "Sân bay đến")]
        [ForeignKey("ArrivalAirport")]
        public int ArrivalAirportId { get; set; }

        /// <summary>
        /// Navigation Property - Sân bay đích
        /// Tương ứng với ArrivingFlights bên Airport (thông qua [InverseProperty])
        /// </summary>
        public virtual Airport? ArrivalAirport { get; set; }

        // ========================================
        // GIẢI THÍCH:
        // ========================================
        // Flight có 2 FK trỏ đến Airport:
        // - DepartureAirportId → DepartureAirport (Sân bay đi)
        // - ArrivalAirportId → ArrivalAirport (Sân bay đến)
        //
        // Nhờ [InverseProperty] bên Airport:
        // - DepartureAirport ↔ DepartingFlights
        // - ArrivalAirport ↔ ArrivingFlights
        //
        // EF Core hiểu rõ ràng 2 mối quan hệ này!
    }
}
