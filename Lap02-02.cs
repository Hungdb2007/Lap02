using System;
using System.Collections;
using System.Collections.Generic;

namespace CSLab02
{
    // Custom Exception for Exercise 1
    public class NotNegativeException : Exception
    {
        public NotNegativeException() : base("Gia tri duoi dau can < 0") { }
        public NotNegativeException(string message) : base(message) { }
    }

    // Structure for Exercise 3
    public struct MatHang
    {
        public int MaMH;
        public string TenMH;
        public int SoLuong;
        public float DonGia;

        public MatHang(int maMH, string tenMH, int soLuong, float donGia)
        {
            this.MaMH = maMH;
            this.TenMH = tenMH;
            this.SoLuong = soLuong;
            this.DonGia = donGia;
        }

        public float ThanhTien()
        {
            return SoLuong * DonGia;
        }

        public override string ToString()
        {
            return string.Format("Ma: {0} | Ten: {1} | So luong: {2} | Don gia: {3:N0} VND | Thanh tien: {4:N0} VND",
                MaMH, TenMH, SoLuong, DonGia, ThanhTien());
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("================ LAB 02 - COLLECTION & EXCEPTION ================");
                Console.WriteLine("1. Bai tap 1: Tinh bieu thuc H = Sqrt((3x + 2y) / (2x - y)) con xu ly Ngoai le");
                Console.WriteLine("2. Bai tap 2: Phat sinh day so nguyen va sap xep tang dan (Dung List)");
                Console.WriteLine("3. Bai tap 3: Quan ly danh sach Mat hang (Dung Hashtable)");
                Console.WriteLine("0. Thoat");
                Console.Write("Chon bai tap (0-3): ");

                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        BaiTap1();
                        break;
                    case "2":
                        BaiTap2();
                        break;
                    case "3":
                        BaiTap3();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Lua chon khong hop le!");
                        break;
                }

                Console.WriteLine("\nNhan phim bat ky de tiep tuc...");
                Console.ReadKey();
            }
        }

        #region Bai tap 1
        static void BaiTap1()
        {
            Console.WriteLine("--- BAI TAP 1: TINH BIEU THUC MATEMATICS VOI EXCEPTION ---");
            try
            {
                Console.Write("Nhap x: ");
                if (!double.TryParse(Console.ReadLine(), out double x))
                {
                    throw new FormatException("Gia tri cua x phai la so!");
                }

                Console.Write("Nhap y: ");
                if (!double.TryParse(Console.ReadLine(), out double y))
                {
                    throw new FormatException("Gia tri cua y phai la so!");
                }

                double mauSo = 2 * x - y;
                if (mauSo == 0)
                {
                    throw new DivideByZeroException("Loi chia cho 0: Mau so (2x - y) bang 0!");
                }

                double tuSo = 3 * x + 2 * y;
                double trongCan = tuSo / mauSo;

                if (trongCan < 0)
                {
                    throw new NotNegativeException();
                }

                double H = Math.Sqrt(trongCan);
                Console.WriteLine($"\nKet qua bieu thuc H = {H:F4}");
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"[FormatException] {ex.Message}");
            }
            catch (DivideByZeroException ex)
            {
                Console.WriteLine($"[DivideByZeroException] {ex.Message}");
            }
            catch (NotNegativeException ex)
            {
                Console.WriteLine($"[NotNegativeException] {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Ngoai le khac] {ex.Message}");
            }
        }
        #endregion

        #region Bai tap 2
        static void BaiTap2()
        {
            Console.WriteLine("--- BAI TAP 2: PHAT SINH DAY SO VA SAP XEP (LIST) ---");
            try
            {
                Console.Write("Nhap so luong phan tu n (n > 0): ");
                int n = int.Parse(Console.ReadLine());

                if (n <= 0)
                {
                    Console.WriteLine("So luong phan tu n phai lon hon 0!");
                    return;
                }

                List<int> list = new List<int>();
                Random rnd = new Random((int)DateTime.Now.Ticks);

                for (int i = 0; i < n; i++)
                {
                    list.Add(rnd.Next(100)); // Phat sinh so nguyen tu 0 den 99
                }

                Console.WriteLine("\nDay so ban dau:");
                Console.WriteLine(string.Join(" ", list));

                // Sap xep tang dan
                list.Sort();

                Console.WriteLine("\nDay so sau khi sap xep tang dan:");
                Console.WriteLine(string.Join(" ", list));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Loi: {ex.Message}");
            }
        }
        #endregion

        #region Bai tap 3
        static Hashtable danhSachMatHang = new Hashtable();

        static void BaiTap3()
        {
            Console.WriteLine("--- BAI TAP 3: QUAN LY DANH SACH MAT HANG (HASHTABLE) ---");
            while (true)
            {
                Console.WriteLine("\n--- MENU MAT HANG ---");
                Console.WriteLine("1. Nhap va them mat hang");
                Console.WriteLine("2. Xuat danh sach mat hang");
                Console.WriteLine("3. Tim va xoa mat hang theo ma");
                Console.WriteLine("0. Quay lai menu chinh");
                Console.Write("Chon (0-3): ");
                string subChoice = Console.ReadLine();

                switch (subChoice)
                {
                    case "1":
                        NhapDanhSachMatHang();
                        break;
                    case "2":
                        XuatDanhSachMatHang();
                        break;
                    case "3":
                        TimVaXoaMatHang();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Lua chon khong hop le.");
                        break;
                }
            }
        }

        static void ThemMatHang(Hashtable danhsach, MatHang m)
        {
            if (danhsach.ContainsKey(m.MaMH))
            {
                Console.WriteLine($"Ma mat hang {m.MaMH} da ton tai trong danh sach!");
            }
            else
            {
                danhsach.Add(m.MaMH, m);
                Console.WriteLine("Them mat hang thanh cong!");
            }
        }

        static bool TimMatHang(Hashtable danhsach, int maMH)
        {
            return danhsach.ContainsKey(maMH);
        }

        static void XoaMatHang(Hashtable danhsach, int maMH)
        {
            if (TimMatHang(danhsach, maMH))
            {
                danhsach.Remove(maMH);
                Console.WriteLine($"Da xoa mat hang co ma {maMH}.");
            }
            else
            {
                Console.WriteLine($"Khong tim thay mat hang co ma {maMH}.");
            }
        }

        static void NhapDanhSachMatHang()
        {
            string tiepTuc = "Y";
            do
            {
                try
                {
                    Console.Write("\nNhap MaMH (So nguyen): ");
                    int maMH = int.Parse(Console.ReadLine());

                    Console.Write("Nhap TenMH: ");
                    string tenMH = Console.ReadLine();

                    Console.Write("Nhap SoLuong: ");
                    int soLuong = int.Parse(Console.ReadLine());

                    Console.Write("Nhap DonGia: ");
                    float donGia = float.Parse(Console.ReadLine());

                    MatHang m = new MatHang(maMH, tenMH, soLuong, donGia);
                    ThemMatHang(danhSachMatHang, m);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Loi nhap lieu: " + ex.Message);
                }

                Console.Write("Ban co muon tiep tuc nhap (Y/N)? ");
                tiepTuc = Console.ReadLine().Trim().ToUpper();
            } while (tiepTuc == "Y");
        }

        static void XuatDanhSachMatHang()
        {
            Console.WriteLine("\n===== DANH SACH MAT HANG =====");
            if (danhSachMatHang.Count == 0)
            {
                Console.WriteLine("Danh sach dang rong!");
                return;
            }

            foreach (DictionaryEntry item in danhSachMatHang)
            {
                MatHang mh = (MatHang)item.Value;
                Console.WriteLine(mh.ToString());
            }
        }

        static void TimVaXoaMatHang()
        {
            try
            {
                Console.Write("\nNhap ma mat hang can tim va xoa: ");
                int maMH = int.Parse(Console.ReadLine());

                if (TimMatHang(danhSachMatHang, maMH))
                {
                    MatHang mh = (MatHang)danhSachMatHang[maMH];
                    Console.WriteLine("Da tim thay mat hang: " + mh.ToString());
                    XoaMatHang(danhSachMatHang, maMH);

                    Console.WriteLine("\nDanh sach sau khi xoa:");
                    XuatDanhSachMatHang();
                }
                else
                {
                    Console.WriteLine($"Khong tim thay mat hang co ma {maMH} trong danh sach!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Loi: " + ex.Message);
            }
        }
        #endregion
    }
}