using Discount.Grpc.Data;
using Discount.Grpc.Models;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Services;

public class DiscountService(DiscountContext dbcontext, ILogger<DiscountService> logger)
    : DiscountProtoService.DiscountProtoServiceBase
{
    public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
        var coupon = await dbcontext.Coupons
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.ProductName == request.ProductName);

        if (coupon == null)
        {
            logger.LogWarning("Discount is not found for ProductName : {ProductName}", request.ProductName);
            coupon = new Coupon { ProductName = "No Discount", Amount = 0, Description = "No Discount Desc" };
            return new CouponModel();
        }

        logger.LogInformation("Discount is retrieved for ProductName : {ProductName}, Amount : {Amount}", coupon.ProductName, coupon.Amount);

        var couponModel = coupon.Adapt<CouponModel>();

        return couponModel;
    }

    public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
    {
        var coupon = request.Coupon.Adapt<Coupon>();

        if (coupon is null)
        {
            logger.LogError("Invalid Request");
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid Request"));
        }

        await dbcontext.Coupons.AddAsync(coupon);

        await dbcontext.SaveChangesAsync();

        logger.LogInformation("Discount is successfully created. ProductName : {ProductName}", coupon.ProductName);

        var couponModel = coupon.Adapt<CouponModel>();

        return couponModel;
    }

    public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
    {
        if (request is null)
        {
            logger.LogError("Invalid Request");
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid Request"));
        }

        var updateCoupon = await dbcontext.Coupons.SingleOrDefaultAsync(c => c.Id == request.Coupon.Id);

        var updatedCoupon = request.Coupon.Adapt(updateCoupon);

        await dbcontext.SaveChangesAsync();

        logger.LogInformation("Discount is successfully updated. ProductName : {ProductName}", updatedCoupon.ProductName);

        var couponModel = updatedCoupon.Adapt<CouponModel>();

        return couponModel;


    }

    public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
    {
        if (request is null)
        {
            logger.LogError("Invalid Request");
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid Request"));
        }

        var coupon = dbcontext.Coupons.SingleOrDefault(c => c.ProductName == request.ProductName);

        if (coupon is null)
        {
            logger.LogWarning("Discount is not found for ProductName : {ProductName}", request.ProductName);
            return new DeleteDiscountResponse { Success = false };
        }

        dbcontext.Coupons.Remove(coupon);

        await dbcontext.SaveChangesAsync();

        logger.LogInformation("Discount is successfully deleted. ProductName : {ProductName}", request.ProductName);

        return new DeleteDiscountResponse { Success = true };

    }
}
