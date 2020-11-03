//корзина
function eventModal()
{
	open.classList.toggle("on-click");
}

const cardBtn = document.querySelector("#basket");
const open = document.querySelector(".modal");
const close = document.querySelector(".btn-close");
const cancel = document.querySelector(".cancel");

cardBtn.addEventListener("click", eventModal);

close.addEventListener("click", eventModal);

cancel.addEventListener("click", eventModal)
//длинна анимации
new WOW().init();


// basket

const foodCounter = document.getElementById