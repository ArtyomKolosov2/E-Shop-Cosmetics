function addCostToOrder(cost) {
    const products = document.getElementById('total-cost');
    const block = document.createElement('div');
    block.innerHTML = `
    <div class="total-cost">
        <span>Итого:</span><span>${cost}</span>
    </div>
    `;
    products.appendChild(block);
}

function addProductToOrder(product) {
    const products = document.getElementById('cart_info_body');
    const block = document.createElement('div');
    block.innerHTML = `
    <div class="cart-info__row">
        <span>Имя товара:</span><span>${product.name}</span>
    </div>
    <div class="cart-info__row">
        <span>Количество:</span><span>${product.number}</span>
    </div>
    <div class="cart-info__row">
        <span>Стоимость:</span><span>${product.cost} br</span>
    </div>
    <hr class="hr"/>
    `;
    products.appendChild(block);
}

function makingOrderProducts(products) {
    for (let i = 0; i < products.length; i++) {
        addProductToOrder(products[i]);
    }
}
function makingOrderTotalCost(pricetag) {
    addCostToOrder(pricetag);
}

export default async function makingOrder(getCookie) {
    const products = JSON.parse(getCookie("products"));
    const pricetag = getCookie("pricetag");

    await makingOrderProducts(products);
    await makingOrderTotalCost(pricetag);
}