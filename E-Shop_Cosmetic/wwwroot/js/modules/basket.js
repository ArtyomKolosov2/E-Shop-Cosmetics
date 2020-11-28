function Sum(arr) {
    let sum = 0;
    for (let el of arr) {
        sum += el;
    }
    return sum;
}
export function basketLogic() {
    $().ready(function () {
        $('.btn-counter').click(function (e) {
            const FoodCounter = document.querySelectorAll('.food-counter');
            const FoodPrice = document.querySelectorAll('.food-price');
            let arrFoodPrice = [];
            for (let i = 0; i < FoodPrice.length; i++) {
                arrFoodPrice.push( Number(FoodPrice[i].textContent.slice(0, -2)) );
            }
            let index = FoodPrice(e.target);
            if (this.textContent === '+') {
                FoodCounter[index].innerHTML++;
            }
            else {
                FoodCounter[index].innerHTML--;
            }
            FoodPrice[index].innerHTML = arrFoodPrice[index] * FoodCounter[index].innerHTML;
            
            let priceTag = Sum(arrFoodPrice) | null;

            document.querySelector('.modal-pricetag').innerHTML = `${priceTag} ₽`;
        });
    });
}