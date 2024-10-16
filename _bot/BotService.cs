using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Exceptions;
using System.Net;

public class BotService
{
    private readonly TelegramBotClient _botClient;
    private readonly string _botToken;

    public BotService(string botToken)
    {
        _botToken = botToken;
        _botClient = new TelegramBotClient(botToken);

        // Настройка параметров обработки
        var receiverOptions = new ReceiverOptions
        {
            AllowedUpdates = Array.Empty<UpdateType>() // Получаем все обновления
        };

        // Запуск процесса получения обновлений
        _botClient.StartReceiving(
            HandleUpdateAsync,
            HandleErrorAsync,
            receiverOptions
        );

        Console.WriteLine("Bot is running...");

        HttpListener server = new HttpListener();
        // установка адресов прослушки
        //server.Prefixes.Add("http://127.0.0.1:8080/connection/");
        //server.Start(); // начинаем прослушивать входящие подключения
        //WaitAndHandleRequest(server, _botClient, new ChatId());

        Console.WriteLine("Запрос обработан");

        Task.Delay(Timeout.Infinite).Wait();
    }

    private async Task WaitAndHandleRequest(HttpListener server, ITelegramBotClient botClient, ChatId chatId)
    {
        var context = await server.GetContextAsync();

        foreach (var header in context.Request.Headers)
        {
            await botClient.SendTextMessageAsync(chatId, $"{header}");
        }

        WaitAndHandleRequest(server, botClient, chatId);
    }

    // Асинхронный обработчик для сообщений и других обновлений
    private async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        try
        {
            if (update.Message is { Text: { } } message)
            {
                // Логируем сообщение
                //Console.WriteLine($"Received message from {message.From?.Username}: {message.Text}");

                // Отправляем ответное сообщение
                //await botClient.SendTextMessageAsync(new ChatId(1371948480), $"and {message.Text}", cancellationToken: cancellationToken);
            }
            else if (update.PreCheckoutQuery is { } preCheckoutQuery)
            {
                // Обрабатываем запрос на оплату
                if (preCheckoutQuery.Currency == "XTR")
                {
                    await botClient.AnswerPreCheckoutQueryAsync(preCheckoutQuery.Id, cancellationToken: cancellationToken);
                }
                else
                {
                    await botClient.AnswerPreCheckoutQueryAsync(preCheckoutQuery.Id, "Invalid order", cancellationToken: cancellationToken);
                }
            }
            else if (update.Message?.SuccessfulPayment is { } successfulPayment)
            {
                try
                {
                    // Обрабатываем успешную оплату
                    string logMessage = $"\n{DateTime.Now}: " +
                        $"User {update.Message.From?.Username} paid for {successfulPayment.InvoicePayload}: " +
                        $"{successfulPayment.TelegramPaymentChargeId} {successfulPayment.ProviderPaymentChargeId}";

                    // Используем System.IO.File
                    await System.IO.File.AppendAllTextAsync("payments.log", logMessage, cancellationToken);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to write payment log: {ex.Message}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error handling update: {ex.Message}");
        }
    }

    // Обработчик ошибок
    private Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        var errorMessage = exception switch
        {
            ApiRequestException apiRequestException
                => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
            _ => exception.ToString()
        };

        Console.WriteLine(errorMessage);
        return Task.CompletedTask;
    }
}