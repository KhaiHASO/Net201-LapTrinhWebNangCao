using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demo03.Models
{
    /// <summary>
    /// Entity đại diện cho Sân bay
    /// Minh họa: InverseProperty - Xử lý nhiều quan hệ giữa 2 thực thể
    /// </summary>
    [Table("Airports")]
    public class Airport
    {
        [Key]
        public int AirportId { get; set; }

        [Required(ErrorMessage = "Mã sân bay là bắt buộc")]
        [StringLength(10, ErrorMessage = "Mã sân bay không được vượt quá 10 ký tự")]
        [Display(Name = "Mã sân bay")]
        public string Code { get; set; } = string.Empty;

        [Required(ErrorMessage = "Tên sân bay là bắt buộc")]
        [StringLength(200, ErrorMessage = "Tên sân bay không được vượt quá 200 ký tự")]
        [Display(Name = "Tên sân bay")]
        public string Name { get; set; } = string.Empty;

        [StringLength(100)]
        [Display(Name = "Thành phố")]
        public string? City { get; set; }

        [StringLength(100)]
        [Display(Name = "Quốc gia")]
        public string? Country { get; set; }

        // ========================================
        // INVERSE PROPERTY - ĐIỂM QUAN TRỌNG!
        // ========================================

        /// <summary>
        /// Danh sách các chuyến bay ĐI TỪ sân bay này
        /// [InverseProperty] chỉ định rõ ràng collection này map với property "DepartureAirport" bên Flight
        /// </summary>
        [InverseProperty("DepartureAirport")]
        public virtual ICollection<Flight> DepartingFlights { get; set; } = new List<Flight>();

        /// <summary>
        /// Danh sách các chuyến bay ĐẾN sân bay này
        /// [InverseProperty] chỉ định rõ ràng collection này map với property "ArrivalAirport" bên Flight
        /// </summary>
        [InverseProperty("ArrivalAirport")]
        public virtual ICollection<Flight> ArrivingFlights { get; set; } = new List<Flight>();

        // ========================================
        // GIẢI THÍCH:
        // ========================================
        // Nếu KHÔNG có [InverseProperty]:
        // - EF Core sẽ KHÔNG BIẾT DepartingFlights map với DepartureAirport hay ArrivalAirport
        // - EF Core sẽ KHÔNG BIẾT ArrivingFlights map với DepartureAirport hay ArrivalAirport
        // - Kết quả: Lỗi hoặc tạo thêm FK không mong muốn
        //
        // Với [InverseProperty]:
        // - [InverseProperty("DepartureAirport")] => DepartingFlights map với DepartureAirport
        // - [InverseProperty("ArrivalAirport")] => ArrivingFlights map với ArrivalAirport
        // - EF Core hiểu rõ ràng 2 mối quan hệ khác nhau!
    }
}
