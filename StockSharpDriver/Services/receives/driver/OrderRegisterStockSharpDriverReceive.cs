﻿////////////////////////////////////////////////
// © https://github.com/badhitman - @FakeGov 
////////////////////////////////////////////////

using SharedLib;

namespace Transmission.Receives.StockSharpDriver;

/// <summary>
/// OrderRegisterStockSharpDriverReceive
/// </summary>
public class OrderRegisterStockSharpDriverReceive(IDriverStockSharpService ssRepo)
    : IMQTTReceive<CreateOrderRequestModel, ResponseBaseModel>
{
    /// <inheritdoc/>
    public static string QueueName => GlobalStaticConstantsTransmission.TransmissionQueues.OrderRegisterStockSharpReceive;

    /// <inheritdoc/>
    public async Task<ResponseBaseModel> ResponseHandleActionAsync(CreateOrderRequestModel req, CancellationToken token = default)
    {
        if (req is null)
            throw new ArgumentNullException(nameof(req));

        return await ssRepo.OrderRegisterAsync(req, token);
    }
}