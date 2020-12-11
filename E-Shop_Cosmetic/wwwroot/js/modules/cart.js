function delElement(arr, index)
{
    let newArr = [];
    for (let i in arr) {
        if (i == index) {
            continue;
        }
        newArr.push(arr[i]);
    }
    return newArr;
}

function addProduct(product) {
    const products = document.getElementById('products');
    const block = document.createElement('div');
    block.innerHTML = `
    <div class="food-row">
        <button class="btn-counter-close" id="btn_delete_food_row">&times;</button>
        <span class="food-name">${product.name}</span>
        <strong class="food-price">${product.cost} br</strong>
        <div class="food-counter">
            <button class="btn-counter" id="btn-counter">-</button>
            <span class="counter" min="0" max="10000">${product.number}</span>
            <button class="btn-counter" id="btn-counter">+</button>
        </div>
    </div>
    `;
    products.appendChild(block);
}

function createProducts(product) {
    for (let i in product) {
        addProduct(product[i]);
    }
}

function sumProducts(arrFoodPrice, foodPrice) {
    let sum = 0;
    for (let i = 0; i < foodPrice.length; i++) {
        sum += arrFoodPrice[i] * foodPrice[i].innerHTML;
    }
    return sum;
}

function parseFoodPrice(foodPrice) {
    let arrFoodPrice = []
    for (let i = 0; i < foodPrice.length; i++) {
        arrFoodPrice.push(Number(foodPrice[i].innerHTML.slice(0, -2)));
    }
    return arrFoodPrice;
}

function getPricetag() {
    const foodPrice = document.querySelectorAll('.food-price');
    const counter = document.querySelectorAll('.counter');
    let arrFoodPrice = parseFoodPrice(foodPrice);

    return sumProducts(arrFoodPrice, counter);
}

export function uploadCart(getCookie) {
    const products = getCookie("products") ? JSON.parse(getCookie("products")) : [];
    createProducts(products);

    const pricetag = getCookie("pricetag") ? getCookie("pricetag") : 0;
    document.querySelector('.modal-pricetag').innerHTML = `${pricetag} br`;
}

export function cartHandler(getCookie, setCookie) {
    $().ready(function () {
        $('.btn-counter-close').on('click', function () {
            let parent = $(this).parent();
            let index = Math.round($('.food-row').find('button').parent().index(parent) / 2);

            const foodRow = document.querySelectorAll(".food-row");
            foodRow[index].remove();

            let allProducts = getCookie("products") ? JSON.parse(getCookie("products")) : [];
            allProducts = delElement(allProducts, index);

            const pricetag = Math.round(getPricetag() * 100) / 100;
            const modalPricetag = document.querySelector('.modal-pricetag');

            modalPricetag.innerHTML = `${pricetag} br`;
            setCookie("products", JSON.stringify(allProducts));
            setCookie("pricetag", pricetag);
        });
        $('.btn-counter').on('click', function () {
            const newProducts = getCookie("products") ? JSON.parse(getCookie("products")) : [];
            const counter = document.querySelectorAll('.counter');

            let parent = $(this).parent();
            let index = $('.food-counter').find('button').parent().index(parent);

            if (this.textContent === '+') {
                counter[index].innerHTML++;
            }
            else {
                counter[index].innerHTML--;
                if (counter[index].innerHTML < 1) {
                    counter[index].innerHTML = 1;
                }
            }
            const pricetag = Math.round(getPricetag() * 100) / 100;
            const modalPricetag = document.querySelector('.modal-pricetag');

            modalPricetag.innerHTML = `${pricetag} br`;

            newProducts[index].number = Number(counter[index].innerHTML);

            setCookie("products", JSON.stringify(newProducts));
            setCookie("pricetag", pricetag);
        });
    });
}

function isContained(allProducts, product) {
    for (let i = 0; i < allProducts.length; i++) {
        if (allProducts[i]["id"] === product["id"]) {
            return true;
        }
    }
    return false;
}

export function addToCart(getCookie, setCookie)
{
    const productName = document.querySelector('.product-info__header');
    const cost = document.querySelector('.offer-footer__cost');
    let counter = document.querySelectorAll('.counter');

    const id = Number(document.getElementById('id_product').innerHTML);
    const product = new Object();
    product["name"] = productName.innerHTML;
    product["cost"] = Number(cost.innerHTML.slice(0, -2));
    product["id"] = id;

    let allProducts = getCookie("products") ? JSON.parse(getCookie("products")) : [];

    const index = allProducts.length - 1;
    // The product is contained in cart
    if (isContained(allProducts, product)) {
        counter[index].innerHTML = Number(counter[index].innerHTML) + 1;
        product["number"] = Number(counter[index].innerHTML);
        allProducts[index]["number"] = product["number"];
    } else {
        product["number"] = 1;
        addProduct(product);// add to basket
        allProducts.push(product); // push to array objects

        location.reload();
    }

    let pricetag = Math.round(getPricetag() * 100) / 100;
    document.querySelector('.modal-pricetag').innerHTML = `${pricetag} br`;

    setCookie("products", JSON.stringify(allProducts));
    setCookie("pricetag", pricetag);

    console.log(allProducts);
}