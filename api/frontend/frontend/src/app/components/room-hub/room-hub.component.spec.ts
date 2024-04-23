import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RoomHubComponent } from './room-hub.component';

describe('RoomHubComponent', () => {
  let component: RoomHubComponent;
  let fixture: ComponentFixture<RoomHubComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RoomHubComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(RoomHubComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
