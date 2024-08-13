import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TiendaformComponent } from './tiendaform.component';

describe('TiendaformComponent', () => {
  let component: TiendaformComponent;
  let fixture: ComponentFixture<TiendaformComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [TiendaformComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TiendaformComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
