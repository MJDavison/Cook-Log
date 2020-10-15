import { Observable } from 'rxjs';

export interface IMenuItemsClient{


}
export interface IMenuItemDto{
  id?: number;
  listId?: number;
  title?: string |undefined;

}

export class MenuItemDto implements IMenuItemDto{
  id?: number;
  listId?: number;
  title?: string|undefined;

  constructor(data?: IMenuItemDto) {
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
      this.listId = _data["listId"];
      this.title = _data["title"];
    }
  }

  static fromJS(data:any) : MenuItemDto{
    data = typeof data ==='object' ? data : {};
    let result = new MenuItemDto();
    result.init(data);
    return result;
  }

  toJSON(data?: any){
    data = typeof data === 'object' ? data:{};
    data["id"] = this.id;
    data["listId"] = this.listId;
    data["title"] = this.title;
    return data;
  }
}


