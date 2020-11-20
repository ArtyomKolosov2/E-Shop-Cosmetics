function SumArr(arr) {
    let sum = 0;
    for (let el in arr) {
        sum += Number(arr[el].textContent);
    }
    return sum;
}

export function basketLogic() {
    $().ready(function () {
        $('.btn-counter').click(function () {
            const arrConters = document.querySelectorAll('.food-counter');
            const arrFoodPrice = document.querySelectorAll('.food-price');
            parseFoodPrice(arrFoodPrice);
            const index = this.index | 0;
            if (this.textContent === '+') {
                arrConters[index]++;
            }
            else {
                arrConters[index]--;
            }
            for (let i = 0; i < arrConters.length - 1; i++) {
                arrFoodPrice[i].innerHTML = arrFoodPrice[i] * arrConters[i];
            }
            
            let priceTag = SumArr(arrFoodPrice) | null;

            document.querySelector('.modal-pricetag').innerHTML = `${priceTag} ₽`;
        });
    });
}
export function basketData() {
    foodPrice = [5, 4.99, 2, 3, 2.45, 1.49];
    conter = [0, 0, 0, 0, 0, 0];
    cost = 0;
}