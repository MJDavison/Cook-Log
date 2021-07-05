import { Component, OnInit, TemplateRef } from '@angular/core';
import { CreateMealCategoryCommand, CreateMealItemCommand, MealCategoriesClient, MealCategoryDto, MealItemDto, MealItemsClient, MealsVm, UpdateMealCategoryCommand, UpdateMealItemCommand, UpdateMealItemDetailCommand } from 'src/api/web-api-client';
import { BsModalRef, BsModalService } from "ngx-bootstrap/modal";
import { faEllipsisH, faPlus } from '@fortawesome/free-solid-svg-icons';


@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {

  debug = false;
  vm: MealsVm

  selectedCategory: MealCategoryDto;
  selectedItem: MealItemDto;

  newCategoryEditor: any = {};
  categoryOptionsEditor: any = {};
  itemDetailsEditor: any = {};

  newCategoryModalRef: BsModalRef;
  categoryOptionsModalRef: BsModalRef;
  deleteCategoryModalRef: BsModalRef;
  itemDetailsModalRef: BsModalRef;

  faPlus = faPlus;
  faEllipsisH = faEllipsisH;

  menuItem : MealItemDto;
  constructor(private categoriesClient: MealCategoriesClient, private itemsClient: MealItemsClient, private modalService: BsModalService) {
    categoriesClient.get().subscribe(
      result=>{
        this.vm = result;
        if(this.vm.categories.length){
          this.selectedCategory = this.vm.categories[0];
        }
      },
      error => console.error(error)
    );
  }

  remainingItems(list: MealCategoryDto){
    return list.items.length;
  }

  //Categories
  showNewCategoryModal(template: TemplateRef<any>): void{
    this.newCategoryModalRef = this.modalService.show(template);
    setTimeout(() => document.getElementById("title").focus(), 250);
  }

  newCategoryCancelled(): void{
    this.newCategoryModalRef.hide();
    this.newCategoryEditor = {};
  }

  addCategory(): void{
    let category = MealCategoryDto.fromJS({
      id: 0,
      title: this.newCategoryEditor.title,
      items: [],
    });

    this.categoriesClient.create(<CreateMealCategoryCommand>{title: this.newCategoryEditor.title}).subscribe(
      result =>{
        category.id = result;
        this.vm.categories.push(category);
        this.selectedCategory = category;
        this.newCategoryModalRef.hide();
        this.newCategoryEditor={};
      },
      error=>{
        let errors = JSON.parse(error.response);

        if(errors && errors.Title){
          this.newCategoryEditor.error = errors.Title[0];
        }

        setTimeout(()=> document.getElementById("title").focus(), 250);
      }
    );
  }

  showCategoryOptionsModal(template: TemplateRef<any>){
    this.categoryOptionsEditor ={
      id: this.selectedCategory.id,
      title:this.selectedCategory.title,
    };
    this.categoryOptionsModalRef = this.modalService.show(template);
  }

  updateCategoryOptions(){
    this.categoriesClient.update(this.selectedCategory.id, UpdateMealCategoryCommand.fromJS(this.categoryOptionsEditor))
    .subscribe(
      ()=>{
        this.selectedCategory.title = this.categoryOptionsEditor.title,
        this.categoryOptionsModalRef.hide();
        this.categoryOptionsEditor = {};
      },
      error=>console.error(error)
    );
  }

  confirmDeleteCategory(template: TemplateRef<any>){
    this.categoryOptionsModalRef.hide();
    this.deleteCategoryModalRef = this.modalService.show(template);
  }

  deleteCategoryConfirmed(): void{
    this.categoriesClient.delete(this.selectedCategory.id).subscribe(
      ()=>{
        this.deleteCategoryModalRef.hide();
        this.vm.categories = this.vm.categories.filter(t=>t.id != this.selectedCategory.id)
        this.selectedCategory = this.vm.categories.length ? this.vm.categories[0] : null;
      },
      error=>console.error(error)
    );
  }

  // Items

  showItemDetailsModal(template: TemplateRef<any>, item: MealItemDto): void{
    this.selectedItem = item;
    this.itemDetailsEditor = {
      ...this.selectedItem
    };

    this.itemDetailsModalRef = this.modalService.show(template);
  }

  updateItemDetails(): void{
    this.itemsClient.updateItemDetails(this.selectedItem.id, UpdateMealItemDetailCommand.fromJS(this.itemDetailsEditor)).subscribe(
      ()=>{
        if(this.selectedItem.categoryId != this.itemDetailsEditor.categoryId){
          this.selectedCategory.items = this.selectedCategory.items.filter(i=>i.id != this.selectedItem.id)
          let categoryIndex = this.vm.categories.findIndex(l=> l.id == this.itemDetailsEditor.categoryId);
          this.selectedItem.categoryId = this.itemDetailsEditor.categoryId;
          this.vm.categories[categoryIndex].items.push(this.selectedItem);
        }
        this.itemDetailsModalRef.hide();
        this.itemDetailsEditor = {};
      },
      error=>console.error(error)
    );
  }

  addItem(){
    let item = MealItemDto.fromJS({
      id: 0,
      categoryId: this.selectedCategory.id,
      title: '',
    });

    this.selectedCategory.items.push(item);
    let index = this.selectedCategory.items.length - 1;
    this.editItem(item, 'itemTitle' + index);
  }

  editItem(item: MealItemDto, inputId: string): void{
    this.selectedItem = item;
    setTimeout(()=> document.getElementById(inputId).focus(), 100);
  }

  updateItem(item:MealItemDto, pressedEnter: boolean = false):void{
    let isNewItem = item.id == 0;

    if(!item.title.trim()){
      this.deleteItem(item);
      return;
    }

    if(item.id == 0){
      this.itemsClient.create(CreateMealItemCommand.fromJS({ ...item, categoryId: this.selectedCategory.id}))
      .subscribe(
        result=>{
          item.id = result;
        },
        error=>console.error(error)
      );
    } else{
      this.itemsClient.update(item.id, UpdateMealItemCommand.fromJS(item)).subscribe(
        ()=> console.log('Update suceeded'),
        error=> console.error(error)
      );
    }
    this.selectedItem = null;

    if(isNewItem && pressedEnter){
      this.addItem();
    }
  }

  deleteItem(item:MealItemDto)
  {
    if(this.itemDetailsModalRef){
      this.itemDetailsModalRef.hide();
    }

    if(item.id == 0){
      let itemIndex = this.selectedCategory.items.indexOf(this.selectedItem);
      this.selectedCategory.items.splice(itemIndex, 1);
    } else {
      this.itemsClient.delete(item.id).subscribe(
        ()=> this.selectedCategory.items = this.selectedCategory.items.filter(t=>t.id != item.id),
        error => console.error(error)
      );
    }
  }
  ngOnInit(): void {
  }

}
