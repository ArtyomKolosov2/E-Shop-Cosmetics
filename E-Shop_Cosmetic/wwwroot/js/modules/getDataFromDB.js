export async function setMinMaxPrice() {
    const response = await fetch("/api/ProductsApi/MinMaxPrice", {
        method: "GET",
        headers: { "Accept": "application/json" }
    });
    if (response.ok === true) {
        const user = await response.json();
        const form = document.forms["form_search"];
        form.elements["priceMin"].value = user.min;
        form.elements["priceMax"].value = user.max;
    }
}

export async function getMinPrice() {
    const response = await fetch("/api/ProductsApi/MinPrice", {
        method: "GET",
        headers: { "Accept": "application/json" }
    });
    let data = await response.json();
    return data.min;
}

export async function getMaxPrice() {
    const response = await fetch("/api/ProductsApi/MaxPrice", {
        method: "GET",
        headers: { "Accept": "application/json" }
    });
    let data = await response.json();
    return data.max;
}
