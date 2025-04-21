using Microsoft.Extensions.Configuration;

namespace Audivia.Application.Utils.Helper
{
    public class EmailContent
    {

        public static string EmailOTPContent(string username, int otp)
        {
            return $@"
                    <div style='background-color:#eaf7f6;font-family:sans-serif;padding:20px'>
                      <div style='max-width:600px;margin:auto;background:white;border-radius:8px;padding:30px;color:#003f5c'>
                        <h2 style='color:#1A469E;'>🎧 Audivia - Xác minh tài khoản</h2>
                        <p>Xin chào <strong>{username}</strong>,</p>
                        <p>Bạn vừa yêu cầu mã OTP để xác minh tài khoản của mình tại <strong>Audivia</strong> – nền tảng trải nghiệm du lịch bằng âm thanh.</p>
                        <p style='text-align:center;margin:30px 0'>
                          <span style='font-size:24px;color:#3CB4AC;border:2px dashed #3CB4AC;padding:10px 20px;border-radius:8px;font-weight:bold'>{otp}</span>
                        </p>
                        <p>Nếu bạn không yêu cầu OTP, vui lòng bỏ qua email này.</p>
                        <br>
                        <p>🌴 Trân trọng,<br><strong>Đội ngũ Audivia</strong></p>
                      </div>
                    </div>";
        }
        public static string ConfirmEmail(string username, IConfiguration configuration, string tokenConfirm)
        {
            var frontendUrl = configuration["Frontend:BaseUrl"];

            var confirmLink = $"{frontendUrl}/auth/verify-email?token={tokenConfirm}";
            return $@"
                    <div style='background-color:#eaf7f6;font-family:sans-serif;padding:20px'>
                      <div style='max-width:600px;margin:auto;background:white;border-radius:8px;padding:30px;color:#003f5c'>
                        <h2 style='color:#1A469E;'>🌍 Audivia - Xác nhận đăng ký</h2>
                        <p>Xin chào <strong>{username}</strong>,</p>
                        <p>Bạn đã đăng ký tài khoản tại <strong>Audivia</strong>. Hãy nhấn vào nút bên dưới để xác nhận:</p>
                        <div style='text-align:center;margin:30px 0'>
                          <a href='{confirmLink}' style='background-color:#3CB4AC;color:white;text-decoration:none;padding:12px 25px;border-radius:6px;font-size:16px;font-weight:bold'>
                            XÁC NHẬN TÀI KHOẢN
                          </a>
                        </div>
                        <p>Nếu bạn không thực hiện thao tác này, hãy bỏ qua email.</p>
                        <br>
                        <p>Trân trọng,<br><strong>Audivia Team</strong></p>
                      </div>
                    </div>";
        }
        public static string WelcomeEmail(string username)
        {
            return $@"
                    <div style='background-color:#eaf7f6;font-family:sans-serif;padding:20px'>
                      <div style='max-width:600px;margin:auto;background:white;border-radius:8px;padding:30px;color:#003f5c'>
                        <h2 style='color:#1A469E;'>🌟 Chào mừng đến với Audivia!</h2>
                        <p><strong>Xin chào {username},</strong></p>
                        <p>Cảm ơn bạn đã gia nhập cộng đồng yêu du lịch của <strong>Audivia</strong>.</p>
                        <ul style='line-height:1.7'>
                          <li>🎧 Trải nghiệm các tour du lịch bằng âm thanh hấp dẫn</li>
                          <li>🗺️ Khám phá văn hóa và lịch sử thông qua AI & AR</li>
                          <li>📍 Gợi ý tour cá nhân hóa theo sở thích & vị trí của bạn</li>
                        </ul>
                        <p>Hãy bắt đầu chuyến hành trình đầu tiên ngay hôm nay và biến mỗi chuyến đi thành một cuộc phiêu lưu đáng nhớ.</p>
                        <br>
                        <p>Chúc bạn có những trải nghiệm tuyệt vời!<br><strong>Đội ngũ Audivia</strong></p>
                      </div>
                    </div>";
        }

    }
}
