const counter = document.getElementById('.counter');
const incr = document.getElementById('.incr');
const decr = document.getElementById('.decr');

let count = 0;
incr.addEventListener("click", ()=>{
    count++;
    counter.innerHTML = count;
});

decr.addEventListener("click", ()=>{
    count--;
    counter.innerHTML = count;
});