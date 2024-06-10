using Discount.Grpc.Data;
using Grpc.Core;
using Mapster;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Shared.Exceptions;

namespace Discount.Grpc.Services;

public class DiscountService(
    DiscountContext discountContext
) : DiscountProtoService.DiscountProtoServiceBase
{
    public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
        var coupon = await discountContext.Coupons
            .FirstOrDefaultAsync(c => c.ProductName == request.ProductName);
        if (coupon == null)
        {
            throw new NotFoundException(typeof(CouponModel).Name, coupon?.ProductName!);
        }
        else
        {
            coupon.Amount -= 1;
            await discountContext.SaveChangesAsync();
        }
        return coupon.Adapt<CouponModel>();
    }
    public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
    {
        var coupon = request.Coupon.Adapt<Coupon>();
        discountContext.Coupons.Add(coupon);
        await discountContext.SaveChangesAsync();
        return coupon.Adapt<CouponModel>();
    }
    public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
    {
        var couponNew = request.Coupon;
        var coupon = discountContext.Coupons
            .FirstOrDefault(c => c.ProductName == request.Coupon.ProductName);
        if (coupon == null)
        {
            throw new NotFoundException(typeof(CouponModel).Name, coupon?.ProductName!);
        }
        else
        {
            couponNew.Id = coupon.Id;
            discountContext.Entry(coupon).CurrentValues.SetValues(couponNew);
            await discountContext.SaveChangesAsync();
        }
        return couponNew;
    }
    public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
    {
        var coupon = discountContext.Coupons
            .FirstOrDefault(c => c.ProductName == request.ProductName);
        if (coupon == null)
        {
            throw new NotFoundException(typeof(CouponModel).Name, coupon?.ProductName!);
        }
        else
        {
            discountContext.Coupons.Remove(coupon);
            await discountContext.SaveChangesAsync();
        }
        return new DeleteDiscountResponse()
        {
            Success = true
        };
    }
}