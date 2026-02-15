function tyoeOfInput() {
    const inputField = document.getElementById("inputField").value;
    let numberValue;
    if(inputField.toLowerCase() === "true" || inputField.toLowerCase() === "false") {
        numberValue="boolean";
     }
     else if(!isNaN(Number(inputField)) && inputField.trim() !== "") {
        numberValue="number";
     }
     else {
        numberValue="string";
     }
    var displayHere = document.getElementById("DisplayHere");
    displayHere.innerText = "The type of input is: " + numberValue;
}
function sumOfTwoNumbers() {
    let num1=+document.getElementById("num1").value
    let num2=+document.getElementById("num2").value
    let sum=(num1)+(num2);
    document.getElementById("SumDisplay").innerText="The sum of two numbers is: "+sum;
}
function toggleMessage(){
    let messageDiv=document.getElementById("Toggle");
    if(messageDiv.style.display==="none" || messageDiv.style.display===""){
        messageDiv.style.display="block";
    } else {
        messageDiv.style.display="none";
    }
}
function addFavoriteColor(){
    let colorInput=document.getElementById("colorInput").value;
    let colorList=document.getElementById("colorList");
    let listItem=document.createElement("li");
    listItem.innerText=colorInput;
    colorList.appendChild(listItem);
}
function showStudentInfo(){
     let student=[{
        name:"John Doe",
        age:21,
        grade:"A"},
        {name:" Doe",
        age:21,
        grade:"A"}];
        let buttonhide=document.getElementById("studentInfo");
    let mess=document.getElementById("studentDisplay");
    if(mess.style.display==="none" || mess.style.display===""){
        
       
    let infoDiv=document.getElementById("studentDisplay");
    for(var i=0;i<student.length;i++){
         let listItem=document.createElement("li");
         listItem.innerHTML=`Name: ${student[i].name}, Age: ${student[i].age}, Grade: ${student[i].grade}`;
            infoDiv.appendChild(listItem);
           
            buttonhide.style.display="none";
    }
}
    else{
       
        buttonhide.style.display="none";
    }
    
    
    
}
function changeBackgroundColorRed(){
    document.body.style.backgroundColor="red";
}
function changeBackgroundColorBlue(){
    document.body.style.backgroundColor="blue";
}
function changeBackgroundColorGreen(){
    document.body.style.backgroundColor="green";
}
function resetBackgroundColor(){
    document.body.style.backgroundColor="white";
} 