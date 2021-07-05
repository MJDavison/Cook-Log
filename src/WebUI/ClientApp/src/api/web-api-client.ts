//#region imports
import { HttpClient } from '@angular/common/http';
import { Inject, Injectable, Optional } from '@angular/core';
import { Observable } from 'rxjs';
//#endregion imports

//#region interfaces
export interface IMealItemsClient{
  getMealItemsWithPagination(listId:number | undefined, pageNumber: number|undefined, pageSize:number |undefined): Observable<PaginatedListOfMealItemDto>;
  create(command: CreateMealItemCommand):Observable<number>;
  update(id:number, command: UpdateMealItemCommand): Observable<IFileResponse>;
  delete(id:number): Observable<IFileResponse>;
  updateItemDetails(id:number |undefined, command: UpdateMealItemDetailCommand) : Observable<IFileResponse>;
}
export interface IMealCategoriesClient{
  get(): Observable<MealsVm>;
  create(command: CreateMealCategoryCommand): Observable<number>;
  get2(id:number):Observable<IFileResponse>;
  update(id: number, command:UpdateMealCategoryCommand):Observable<number>;
  delete(id:number): Observable<IFileResponse>;
}
export interface IMealItemDto{
  id?: number;
  categoryId?: number;
  title?: string |undefined;

}
export interface IPaginatedListOfMealItemDto
{
  items?: MealItemDto[] | undefined;
  pageIndex?: number;
  totalPages?: number;
  totalCount?: number;
  hasPreviousPage?:boolean;
  hasNextPage?: boolean;
}
export interface ICreateMealItemCommand
{
  categoryId?: number;
  title?: string | undefined;

}
export interface IUpdateMealItemCommand
{
  id?: number;
  title?: string |undefined
}
export interface IUpdateMealItemDetailCommand
{
  id?: number;
  categoryId?: number;
}
export interface IMealsVm
{
  categories?: MealCategoryDto[] | undefined;
}
export interface ICreateMealCategoryCommand
{
  title?: string | undefined;
}
export interface IUpdateMealCategoryCommand
{
  id?: number;
  title?: string|undefined;
}

export interface IFileResponse {
  data: Blob;
  status: number;
  fileName?:string;
  headers?:{[name: string]: any};
}

export interface IMealCategoryDto{
  id?: number;
  title?: string | undefined;
  items?: MealItemDto[] | undefined;
}
//#endregion interfaces

//#region enums

//#endregion enums

//#region classes
@Injectable({
  providedIn: 'root'
})
export class MealItemsClient implements IMealItemsClient{

  private httpClient: HttpClient;
  private baseUrl : string;
  protected jsonParseReciever: ((key: string, value: any)=> any) | undefined;

  constructor(@Inject(HttpClient) httpClient: HttpClient, @Optional() @Inject('API_BASE_URL') baseUrl?:string) {
    this.httpClient = httpClient;
    this.baseUrl = baseUrl ? baseUrl : "";
  }
  getMealItemsWithPagination(categoryId: number, pageNumber: number, pageSize: number): Observable<PaginatedListOfMealItemDto> {
    throw new Error('Method not implemented.');
  }
  create(command: CreateMealItemCommand): Observable<number> {
    throw new Error('Method not implemented.');
  }
  update(id: number, command: UpdateMealItemCommand): Observable<IFileResponse> {
    throw new Error('Method not implemented.');
  }
  delete(id: number): Observable<IFileResponse> {
    throw new Error('Method not implemented.');
  }
  updateItemDetails(id: number, command: UpdateMealItemDetailCommand): Observable<IFileResponse> {
    throw new Error('Method not implemented.');
  }


}
@Injectable({
  providedIn: 'root'
})
export class MealCategoriesClient implements IMealCategoriesClient{
  get(): Observable<MealsVm> {
    throw new Error('Method not implemented.');
  }
  create(command: CreateMealCategoryCommand): Observable<number> {
    throw new Error('Method not implemented.');
  }
  get2(id: number): Observable<IFileResponse> {
    throw new Error('Method not implemented.');
  }
  update(id: number, command: UpdateMealCategoryCommand): Observable<number> {
    throw new Error('Method not implemented.');
  }
  delete(id: number): Observable<IFileResponse> {
    throw new Error('Method not implemented.');
  }


}
export class MealItemDto implements IMealItemDto{
  id?: number;
  categoryId?: number;
  title?: string|undefined;

  constructor(data?: IMealItemDto) {
    if(data){
      for(var property in data){
        if(data.hasOwnProperty(property))
        (<any>this)[property] = (<any>data)[property];
      }
    }
  }

  init (_data?: any){
    if(_data){
      this.id = _data["id"];
      this.categoryId = _data["categoryId"];
      this.title = _data["title"];
    }
  }

  static fromJS(data:any) : MealItemDto{
    data = typeof data ==='object' ? data : {};
    let result = new MealItemDto();
    result.init(data);
    return result;
  }

  toJSON(data?: any){
    data = typeof data === 'object' ? data:{};
    data["id"] = this.id;
    data["listId"] = this.categoryId;
    data["title"] = this.title;
    return data;
  }
}
export class PaginatedListOfMealItemDto implements IPaginatedListOfMealItemDto
{
  items?: MealItemDto[] | undefined;
  pageIndex?: number;
  totalPages?: number;
  totalCount?: number;
  hasPreviousPage?: boolean;
  hasNextPage?: boolean;

  constructor(data?: IPaginatedListOfMealItemDto) {
    if(data){
      for(var property in data){
        if(data.hasOwnProperty(property)){
          (<any>this)[property] = (<any>data)[property];
        }
      }
    }
  }

  init(_data?: any){
    if(_data){
      if(Array.isArray(_data["items"])){
        this.items = [] as any;
        for(let item of _data["items"]){
          this.items!.push(MealItemDto.fromJS(item));
        }
        this.pageIndex = _data["pageIndex"];
        this.totalPages =_data["totalPages"];
        this.totalCount = _data["totalCount"];
        this.hasPreviousPage = _data["hasPreviousPage"];
        this.hasNextPage = _data["hasNextPage"];
      }
    }
  }

  static fromJS(data: any): PaginatedListOfMealItemDto{
    data = typeof data === 'object' ? data : {};
    let result = new PaginatedListOfMealItemDto();
    result.init(data);
    return result;
  }

  toJson(data?: any){
    data = typeof data === 'object' ? data : {};
    if(Array.isArray(this.items)){
      data["items"] = [];
      for(let item of this.items){
        data["items"].push(item.toJSON());
      }
      data["pageIndex"] = this.pageIndex;
      data["totalPages"] = this.totalPages;
      data["totalCount"] = this.totalCount;
      data["hasPreviousPage"] = this.hasPreviousPage;
      data["hasNextPage"] = this.hasNextPage;
      return data;
    }
  }
}
export class CreateMealItemCommand implements ICreateMealItemCommand
{
  categoryId?: number;
  title?: string|undefined;

  constructor(data?:ICreateMealItemCommand) {
    if(data){
      for(var property in data){
        if(data.hasOwnProperty(property)){
          (<any>this)[property] = (<any>data)[property];
        }
      }
    }
  }

  init(_data: any){
    if(_data){
      this.categoryId = _data["categoryId"];
      this.title = _data["title"];
    }
  }

  static fromJS(data: any): CreateMealItemCommand{
    data = typeof data === 'object' ? data : {};
    let result = new CreateMealItemCommand;
    result.init(data);
    return result;
  }

  toJson(data?: any){
    data = typeof data === 'object' ? data : {};
    data["categoryId"] = this.categoryId;
    data["title"] = this.title;
    return data;
    }
}

export class UpdateMealItemCommand implements IUpdateMealItemCommand
{
    id?: number;
    title?: string | undefined;

    constructor(data?: IUpdateMealItemCommand) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.id = _data["id"];
            this.title = _data["title"];

        }
    }

    static fromJS(data: any): UpdateMealItemCommand {
        data = typeof data === 'object' ? data : {};
        let result = new UpdateMealItemCommand();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["id"] = this.id;
        data["title"] = this.title;
        return data;
    }
}
export class UpdateMealItemDetailCommand implements IUpdateMealItemDetailCommand
{
    id?: number;
    categoryId?: number;

    constructor(data?: IUpdateMealItemDetailCommand) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.id = _data["id"];
            this.categoryId = _data["categoryId"];

        }
    }

    static fromJS(data: any): UpdateMealItemDetailCommand {
        data = typeof data === 'object' ? data : {};
        let result = new UpdateMealItemDetailCommand();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["id"] = this.id;
        data["categoryId"] = this.categoryId;
        return data;
    }
}
export class MealsVm implements IMealsVm
{
  categories?: MealCategoryDto[] | undefined;

    constructor(data?: IMealsVm) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            if (Array.isArray(_data["lists"])) {
                this.categories = [] as any;
                for (let item of _data["lists"])
                    this.categories!.push(MealCategoryDto.fromJS(item));
            }
        }
    }

    static fromJS(data: any): MealsVm {
        data = typeof data === 'object' ? data : {};
        let result = new MealsVm();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        if (Array.isArray(this.categories)) {
            data["lists"] = [];
            for (let item of this.categories)
                data["lists"].push(item.toJSON());
        }
        return data;
    }
}
export class MealCategoryDto implements IMealCategoryDto
{
    id?: number;
    title?: string | undefined;
    items?: MealItemDto[] | undefined;

    constructor(data?: MealCategoryDto) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.id = _data["id"];
            this.title = _data["title"];
            if (Array.isArray(_data["items"])) {
                this.items = [] as any;
                for (let item of _data["items"])
                    this.items!.push(MealItemDto.fromJS(item));
            }
        }
    }

    static fromJS(data: any): MealCategoryDto {
        data = typeof data === 'object' ? data : {};
        let result = new MealCategoryDto();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["id"] = this.id;
        data["title"] = this.title;
        if (Array.isArray(this.items)) {
            data["items"] = [];
            for (let item of this.items)
                data["items"].push(item.toJSON());
        }
        return data;
    }

}
export class CreateMealCategoryCommand implements ICreateMealCategoryCommand
{
  title?: string | undefined;

  constructor(data?: ICreateMealCategoryCommand) {
      if (data) {
          for (var property in data) {
              if (data.hasOwnProperty(property))
                  (<any>this)[property] = (<any>data)[property];
          }
      }
  }

  init(_data?: any) {
      if (_data) {
          this.title = _data["title"];
      }
  }

  static fromJS(data: any): CreateMealCategoryCommand {
      data = typeof data === 'object' ? data : {};
      let result = new CreateMealCategoryCommand();
      result.init(data);
      return result;
  }

  toJSON(data?: any) {
      data = typeof data === 'object' ? data : {};
      data["title"] = this.title;
      return data;
  }
}
export class UpdateMealCategoryCommand implements IUpdateMealCategoryCommand
{
    id?: number;
    title?: string | undefined;

    constructor(data?: IUpdateMealCategoryCommand) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.id = _data["id"];
            this.title = _data["title"];
        }
    }

    static fromJS(data: any): UpdateMealCategoryCommand {
        data = typeof data === 'object' ? data : {};
        let result = new UpdateMealCategoryCommand();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["id"] = this.id;
        data["title"] = this.title;
        return data;
    }
}
//#endregion classes












