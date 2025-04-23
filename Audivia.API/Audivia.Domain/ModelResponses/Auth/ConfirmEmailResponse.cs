namespace Audivia.Domain.ModelResponses.Auth
{
    public static class ConfirmEmailResponse
    {
        public static string VerifyEmailResponse(string mobileAppRedirectLink)
        {
            return $@"
                    <!DOCTYPE html>
                    <html lang='vi'>
                    <head>
                        <meta charset='UTF-8'>
                        <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                        <title>Xác nhận thành công</title>
                        <style>
                            body {{
                                font-family: 'Segoe UI', sans-serif;
                                margin: 0;
                                padding: 0;
                                background: #eaf7f1;
                                color: #333;
                                display: flex;
                                justify-content: center;
                                align-items: center;
                                min-height: 100vh;
                            }}
                            .container {{
                                background: #ffffff;
                                padding: 30px 20px;
                                border-radius: 10px;
                                text-align: center;
                                max-width: 400px;
                                width: 90%;
                                box-shadow: 0 0 10px rgba(0,0,0,0.1);
                            }}
                            h2 {{
                                color: #2c9c7a;
                                margin-bottom: 15px;
                            }}
                            p {{
                                font-size: 16px;
                            }}
                            .button {{
                                margin-top: 25px;
                            }}
                            a {{
                                background-color: #2c9c7a;
                                color: white;
                                text-decoration: none;
                                padding: 12px 20px;
                                border-radius: 8px;
                                font-weight: bold;
                                display: inline-block;
                            }}
                            a:hover {{
                                background-color: #248b6c;
                            }}
                        </style>
                    </head>
                    <body>
                        <div class='container'>
                            <h2>🎉 Xác nhận thành công!</h2>
                            <p>Email của bạn đã được xác minh. Chúc mừng bạn đã đăng ký tài khoản thành công tại <strong>Audivia</strong>.</p>
                            <div class='button'>
                                <a href='{mobileAppRedirectLink}'>Quay lại ứng dụng</a>
                            </div>
                        </div>
                    </body>
                    </html>";
        }
    }
}
